using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

using Mirror;

public class MatchManager : MonoBehaviour
{
    public static event Action OnMatchCanStart;
    public static event Action OnMatchCannotStart;

    private Dictionary<int, PongPlayer> _playerDictionary;
    private MatchController _matchController;
    
    private void OnEnable()
    {
        _playerDictionary = new Dictionary<int, PongPlayer>();

        _matchController = FindFirstObjectByType<MatchController>();

        PongNetworkManager.OnServerAddedPlayer += HandlePlayerAdded;
        PongNetworkManager.OnServerRemovedPlayer += HandlePlayerRemoved;
    }

    private void HandlePlayerAdded(int playerID, PongPlayer player)
    {
        _playerDictionary.Add(playerID, player);

        //Invoke Callback
        if (_playerDictionary.Count > 1)
            OnMatchCanStart?.Invoke();
    }

    private void HandlePlayerRemoved(int playerID)
    {
        _playerDictionary.Remove(playerID);

        //Invoke Callback
        OnMatchCannotStart?.Invoke();
    }

    public void StartMatch()
    {
        Debug.Log("Start Match", this);

        //Convert Dictionary into a List
        List<PongPlayer> pongPlayers = new List<PongPlayer>();
        foreach (var connectedPlayer in _playerDictionary.Values)
            pongPlayers.Add(connectedPlayer);

        //Connect Players
        _matchController.ConnectPlayers(pongPlayers);

        //Start Match Manager Match
        _matchController.StartMatch();
        OnMatchCanStart?.Invoke();
    }
}
