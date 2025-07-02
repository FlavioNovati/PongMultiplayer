using System;

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

using Mirror;

public class GameManager : NetworkBehaviour
{
    [Scene]
    public string gameScene = "";
    [Scene]
    public string gameTitleScene = "";
    
    private bool _canStartMatch = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SceneManager.LoadSceneAsync(gameTitleScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.name));
    }

    #region SERVER
    public override void OnStartServer()
    {
        base.OnStartServer();

        PongNetworkManager.ClientOnConnected += HandleClientConnection;
        PongNetworkManager.ClientOnDisconnected += HandleClientDisconnection;
    }

    [Server]
    private void HandleClientDisconnection()
    {
        if(NetworkManager.singleton.numPlayers >= 2)
            _canStartMatch = true;
        else
        {
            Debug.LogWarning("Stop Match");
            _canStartMatch = false;
        }
    }

    [Server]
    private void HandleClientConnection()
    {
        if (NetworkManager.singleton.numPlayers >= 2)
            _canStartMatch = true;
        else
            _canStartMatch = false;
    }
    #endregion
}
