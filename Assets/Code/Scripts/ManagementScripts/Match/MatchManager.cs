using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

using Mirror;

public class MatchManager : MonoBehaviour
{
    public static event Action OnMatchCanStart;
    public static event Action OnMatchCannotStart;
    public static event Action OnMatchInterrupted;

    private Dictionary<int, PongPlayer> _playerDictionary;
    private MatchController _matchController;
    
    private bool _isPlaying;

    private void OnEnable()
    {
        _playerDictionary = new Dictionary<int, PongPlayer>();

        _matchController = FindFirstObjectByType<MatchController>(FindObjectsInactive.Include);

        PongNetworkManager.OnServerAddedPlayer += HandlePlayerAdded;
        PongNetworkManager.OnServerRemovedPlayer += HandlePlayerRemoved;
    }

    private void OnDisable()
    {
        PongNetworkManager.OnServerAddedPlayer -= HandlePlayerAdded;
        PongNetworkManager.OnServerRemovedPlayer -= HandlePlayerRemoved;
    }

    private void Update()
    {
        if (_isPlaying)
            if (!NetworkClient.isConnected)
                HandleDisconnection();
    }

    private void HandleDisconnection()
    {
        StopMatch();
        OnMatchInterrupted?.Invoke();
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

        if(_isPlaying)
        {
            StopMatch();
            OnMatchInterrupted?.Invoke();
        }
    }

    public void StopMatch()
    {
        Debug.LogError("Player Disconnected - Match stopped");
        _matchController.ResetMatch();
        _isPlaying = false;
        NetworkManager.singleton?.StopHost();
    }

    public void StartMatch()
    {
        //Convert Dictionary into a List
        List<PongPlayer> pongPlayers = new List<PongPlayer>();
        foreach (var connectedPlayer in _playerDictionary.Values)
            pongPlayers.Add(connectedPlayer);

        //Connect Players
        _matchController.ConnectPlayers(pongPlayers);

        //Start Match Manager Match
        _matchController.StartMatch();
        _isPlaying = true;
    }
}
