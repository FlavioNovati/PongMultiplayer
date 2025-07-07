using UnityEngine;
using Mirror;

public class PongRacket : NetworkBehaviour
{
    [SerializeField] private float _speed = 0.15f;
    private PongPlayer _assignedPlayer;
    private Vector3 _startPosition;

    private bool _paused = false;

    public override void OnStartServer()
    {
        _startPosition = transform.position;
        _paused = true;
    }

    [Server]
    public void MoveRacket(Vector3 direction)
    {
        if (_paused)
            return;

        transform.position += direction * _speed * Time.deltaTime;
    }
    
    [Server]
    public void AssignPlayer(PongPlayer player)
    {
        _assignedPlayer = player;
        player.PlayerInput.OnInputDirection += MoveRacket;
    }

    [Server]
    public void DisconnectPlayer() => _assignedPlayer.PlayerInput.OnInputDirection -= MoveRacket;

    [Server]
    public void ResetPosition() => transform.position = _startPosition;

    [Server]
    public void SetPause(bool pause) => _paused = pause;
}
