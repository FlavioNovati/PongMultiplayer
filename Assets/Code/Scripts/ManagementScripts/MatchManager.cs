using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MatchManager : NetworkBehaviour
{
    [SerializeField] private PongRacket _lRacket;
    [SerializeField] private PongRacket _rRacket;
    [SerializeField] private PongBall _ball;

    #region SERVER
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
        _ball.Reset();
        _ball.StartBall();
    }

    [Server]
    public void ResetMatch()
    {
        _ball.Reset();
        //TODO: Reset Rackets
    }
    #endregion
}
