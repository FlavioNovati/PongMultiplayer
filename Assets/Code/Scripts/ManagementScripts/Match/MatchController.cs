using System;
using System.Collections.Generic;

using UnityEngine;

using Mirror;
using Unity.VisualScripting;
using System.Collections;

public class MatchController : NetworkBehaviour
{
    public static event Action OnMatchStop;
    public static event Action OnMatchStart;
    public static event Action<string> OnPlayerWon;

    public delegate void ScoreCallback(int lScore, int rScore);
    public event ScoreCallback OnScoreChanged;

    [SerializeField] private PongRacket _lRacket;
    [SerializeField] private PongRacket _rRacket;
    [SerializeField] private PongBall _ball;
    [Space(15)]
    [SerializeField] private TableBorder _lTableBorder;
    [SerializeField] private TableBorder _rTableBorder;
    [Space(15)]
    [SerializeField] private int _scoreToWin = 11;

    [SyncVar(hook = "UpdateScoreL")]
    private int _lScore;
    [SyncVar(hook = "UpdateScoreR")]
    private int _rScore;
    

    #region SERVER
    [Server]
    public override void OnStartServer()
    {
        _rTableBorder.OnBallCollision += assignScoreL;
        _lTableBorder.OnBallCollision += AssignScoreR;
    }

    [Server]
    public void OnDisable()
    {

        if (connectionToServer == null)
            return;

        _rTableBorder.OnBallCollision -= assignScoreL;
        _lTableBorder.OnBallCollision -= AssignScoreR;
    }

    [Server]
    private void assignScoreL()
    {
        _lScore++;
        InvokeScoreChangeCallback();
        
        if (_lScore >= _scoreToWin)
        {
            StopMatch();
            return;
        }

        StartCoroutine(StartMatchDelay(5f));
    }

    [Server]
    private void AssignScoreR()
    {
        _rScore++;
        InvokeScoreChangeCallback();

        if (_rScore >= _scoreToWin)
        {
            StopMatch();
            return;
        }

        StartCoroutine(StartMatchDelay(5f));
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
        
        StartCoroutine(StartMatchDelay(5f));
        InvokeStartCallback();
    }

    [Server]
    public void StopMatch()
    {
        ResetPositions();
        StartCoroutine(EndMatchDelay(5f));
    }

    [Server]
    public void ResetMatch()
    {
        _lScore = 0;
        _rScore = 0;
        ResetPositions();
        InvokeScoreChangeCallback();
    }

    [Server]
    private void ResetPositions()
    {
        _ball.Reset();
        _lRacket.ResetPosition();
        _rRacket.ResetPosition();
    }

    private IEnumerator StartMatchDelay(float delayTime)
    {
        _lRacket.SetPause(true);
        _rRacket.SetPause(true);

        ResetPositions();
        yield return new WaitForSeconds(delayTime);

        _ball.StartBall();

        _lRacket.SetPause(false);
        _rRacket.SetPause(false);
    }

    private IEnumerator EndMatchDelay(float delayTime)
    {
        _lRacket.SetPause(true);
        _rRacket.SetPause(true);

        ResetPositions();
        yield return new WaitForSeconds(delayTime);
        InvokePlayerWon(_lScore > _rScore ? "Player 0 Won" : "Player 1 Won");
        yield return new WaitForSeconds(delayTime);
        InvokeEndCallback();
    }
    #endregion

    #region CLIENT

    [ClientRpc] private void UpdateScoreL(int oldScore, int newScore) => _lScore = newScore;
    [ClientRpc] private void UpdateScoreR(int oldScore, int newScore) => _rScore = newScore;
    [ClientRpc] private void InvokePlayerWon(string playerName) => OnPlayerWon?.Invoke(playerName);
    [ClientRpc] private void InvokeStartCallback() => OnMatchStart?.Invoke();
    [ClientRpc] private void InvokeEndCallback() => OnMatchStop?.Invoke();
    [ClientRpc] private void InvokeScoreChangeCallback() => OnScoreChanged?.Invoke(_lScore, _rScore);
    #endregion
}
