using Mirror;
using System;
using UnityEngine;

public class PongPlayer : NetworkBehaviour
{
    [SerializeField, SyncVar(hook = "HandleNameChange")] public string PlayerName = "Player";
    [SerializeField, SyncVar] public int PlayerConnectionID = 0;
    [SerializeField] private PlayerInput _playerInput;

    public PlayerInput PlayerInput => _playerInput;

    public override void OnStartServer()
    {
        
    }

    public void HandleNameChange(string oldName,  string newName)
    {
        Debug.Log($"Named changed from {oldName} to {newName}");
        PlayerName = newName;
        this.gameObject.name = newName;
    }

    #region Server

    #endregion


    #region Client
    public override void OnStartAuthority()
    {
        Debug.Log($"-- Authority on {this.name} started", this);
    }

    public override void OnStopAuthority()
    {
        Debug.Log($"-- Authority on {this.name} stopped", this);
    }
    #endregion
}
