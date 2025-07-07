using TMPro;
using UnityEngine;

using Mirror;
using UnityEngine.UI;
using System;

public class UILobbyTab : NetworkBehaviour
{
    [SerializeField] private TMP_Text _player0Text;
    [SerializeField] private TMP_Text _player1Text;
    [SerializeField] private Button _startButton;

    [SerializeField] private Color _connectedColor;
    [SerializeField] private Color _disconnectedColor;

    private bool _isHost;

    #region CLIENT
    private void Awake()
    {
        MatchManager.OnMatchCanStart += EnableButton;
        MatchManager.OnMatchCannotStart += DisableButton;

        PongNetworkManager.OnServerAddedPlayer += HandlePlayerConnected;
        PongNetworkManager.OnServerRemovedPlayer += HandlePlayerDisconnected;
    }

    private void HandlePlayerConnected(int connID, PongPlayer player)
    {
        //logic applied only if player count is greater than 1
        if (NetworkManager.singleton.numPlayers <= 1)
            return;

        _player1Text.color = _connectedColor;
    }

    private void HandlePlayerDisconnected(int connID)
    {
        //logic applied only if player count is greater than 1
        if (NetworkManager.singleton.numPlayers > 1)
            return;

        _player1Text.color = _disconnectedColor;
    }

    private void OnDestroy()
    {
        MatchManager.OnMatchCanStart -= EnableButton;
        MatchManager.OnMatchCannotStart -= DisableButton;
    }

    private void OnEnable()
    {
        DisableButton();
        
        _player0Text.color = _disconnectedColor;
        _player1Text.color = _disconnectedColor;
    }

    private void EnableButton()
    {
        _player1Text.color = _connectedColor;

        if(!_isHost)
            return;

        _startButton.interactable = true;
    }

    private void DisableButton()
    {
        _player1Text.color = _disconnectedColor;
        _startButton.interactable = false;
    }
    #endregion
    
    public void StartGame()
    {
        FindFirstObjectByType<MatchManager>().StartMatch();
    }

    #region SERVER // HOST
    public override void OnStartServer()
    {
        _isHost = true;
        _player0Text.color = _connectedColor;
    }
    #endregion
}
