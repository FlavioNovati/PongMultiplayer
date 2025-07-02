using UnityEngine;

using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PongNetworkManager : NetworkManager
{
    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;

    [Space(15)]
    [SerializeField] private PongPlayer _pongPlayer;

    //Dictionary Connection ID + Player
    Dictionary<int, PongPlayer> _playerDictionary = new Dictionary<int, PongPlayer>();

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        ClientOnConnected?.Invoke();
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        ClientOnDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);

        PongPlayer newPlayer = NetworkManager.Instantiate(_pongPlayer);
        newPlayer.gameObject.name = $"Player [{conn.connectionId}]";
        newPlayer.PlayerName = $"Player{conn.connectionId}";
        newPlayer.PlayerConnectionID = conn.connectionId;
        _playerDictionary.Add(conn.connectionId, newPlayer);

        SceneManager.MoveGameObjectToScene(newPlayer.gameObject, SceneManager.GetActiveScene());

        Debug.Log(conn.connectionId);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        PongPlayer disconnectedPlayer = _playerDictionary[conn.connectionId];
        Destroy(disconnectedPlayer);
        _playerDictionary.Remove(conn.connectionId);

        base.OnServerDisconnect(conn);
    }
}
