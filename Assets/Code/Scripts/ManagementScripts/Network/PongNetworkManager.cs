using System;
using System.Collections.Generic;

using UnityEngine;

using Mirror;

public class PongNetworkManager : NetworkManager
{
    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;
    public static event Action<int, PongPlayer> OnServerAddedPlayer;
    public static event Action<int> OnServerRemovedPlayer;

    [Space(15)]
    [SerializeField] private PongPlayer _pongPlayer;

    //Dictionary Connection ID + Player
    private Dictionary<int, PongPlayer> _playerDictionary = new Dictionary<int, PongPlayer>();

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        ClientOnConnected?.Invoke();
    }
    
    public override void OnStopClient()
    {
        base.OnStopClient();
        ClientOnDisconnected?.Invoke();
    }

    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        if (conn == null)
            return;

        base.OnServerReady(conn);

        //Create Game Object
        PongPlayer newPlayer = NetworkManager.Instantiate(_pongPlayer);
        newPlayer.gameObject.name = $"Player[{conn.connectionId}]";
        newPlayer.PlayerName = $"Player[{conn.connectionId}]";
        newPlayer.PlayerConnectionID = conn.connectionId;
        //Add Player to dictionary
        _playerDictionary.Add(conn.connectionId, newPlayer);

        //Add player to connection dictionary
        NetworkServer.AddPlayerForConnection(conn, newPlayer.gameObject);

        //Invoke Callback
        OnServerAddedPlayer?.Invoke(conn.connectionId, newPlayer);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        //Remove Player From Dictionary
        _playerDictionary.Remove(conn.connectionId);
        OnServerRemovedPlayer?.Invoke(conn.connectionId);
    }
}
