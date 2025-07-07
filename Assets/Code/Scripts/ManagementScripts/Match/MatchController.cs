using System;
using System.Collections.Generic;

using UnityEngine;

using Mirror;

public class MatchController : NetworkBehaviour
{
    public static event Action OnMatchStop;
    public static event Action OnMatchStart;

    public delegate void ScoreCallback(int lScore, int rScore);
    public event ScoreCallback OnScoreChanged;

    [SerializeField] private PongRacket _lRacket;
    [SerializeField] private PongRacket _rRacket;
    [SerializeField] private PongBall _ball;
    [Space(15)]
    [SerializeField] private TableBorder _lTableBorder;
    [SerializeField] private TableBorder _rTableBorder;

    private int _lScore;
    private int _rScore;

    #region SERVER
    [Server]
    public override void OnStartServer()
    {
        _lTableBorder.OnBallCollision += assignScoreL;
        _rTableBorder.OnBallCollision += assignScoreR;
    }

    [Server]
    private void OnDestroy()
    {
        _lTableBorder.OnBallCollision -= assignScoreL;
        _rTableBorder.OnBallCollision -= assignScoreR;
    }

    [Server]
    private void assignScoreL()
    {
        _lScore++;
        InvokeScoreChangeCallback();
    }
    [Server]
    private void assignScoreR()
    {
        _rScore++;
        InvokeScoreChangeCallback();
    }

    [Server]
    public void ConnectPlayers(List<PongPlayer> players)
    {
        _lRacket.AssignPlayer(players[0]);
        _rRacket.AssignPlayer(players[1]);
    }

    [Server]
    public void DisconnectPlayers()
    {
        _lRacket.DisconnectPlayer();
        _rRacket.DisconnectPlayer();
    }
    
    [Server]
    public void StartMatch()
    {
        _lScore = 0;
        _rScore = 0;

        _ball.Reset();
        _ball.StartBall();
        InvokeStartCallback();
        Debug.Log("Start Match", this);
    }

    [Server]
    public void StopMatch()
    {
        _ball.Reset();
        OnMatchStop?.Invoke();
    }

    [Server]
    public void ResetMatch()
    {
        _lScore = 0;
        _rScore = 0;
        ResetPositions();
        //TODO: Reset Rackets

        InvokeScoreChangeCallback();
    }

    [Server]
    private void ResetPositions()
    {
        _ball.Reset();
        //TODO: Reset Rackets
    }
    #endregion

    #region CLIENT
    [ClientRpc]
    private void InvokeStartCallback() => OnMatchStart?.Invoke();
    [ClientRpc]
    private void InvokeScoreChangeCallback() => OnScoreChanged?.Invoke(_lScore, _rScore);
    #endregion
}
