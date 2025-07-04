using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

using Mirror;

public class GameManager : NetworkBehaviour
{
    public static event Action OnMatchStop;
    public static event Action OnMatchStart;
    public static event Action OnMatchCanStart;
    public static event Action OnMatchCannotStart;

    private Dictionary<int, PongPlayer> _playerDictionary;
    private MatchManager _matchManager;

    #region SERVER
    public override void OnStartServer()
    {
        _playerDictionary = new Dictionary<int, PongPlayer>();

        _matchManager = FindFirstObjectByType<MatchManager>();

        PongNetworkManager.OnServerAddedPlayer += HandlePlayerAdded;
        PongNetworkManager.OnServerRemovedPlayer += HandlePlayerRemoved;
    }

    private void HandlePlayerAdded(int playerID, PongPlayer player)
    {
        _playerDictionary.Add(playerID, player);

        //Invoke Callback
        if (_playerDictionary.Count > 1)
            StartMatch();
    }

    private void StartMatch()
    {
        Debug.Log("Start Match", this);

        //Get Players
        List<PongPlayer> pongPlayers = new List<PongPlayer>();
        foreach (var connectedPlayer in _playerDictionary.Values)
            pongPlayers.Add(connectedPlayer);

        //Connect Players
        _matchManager.ConnectPlayers(pongPlayers);

        //Start Match Manager Match
        _matchManager.StartMatch();
        OnMatchCanStart?.Invoke();
    }

    private void HandlePlayerRemoved(int playerID)
    {
        _playerDictionary.Remove(playerID);

        //Invoke Callback
        OnMatchCannotStart?.Invoke();
    }
    #endregion
}
