using UnityEngine;
using Mirror;

public class PongRacket : NetworkBehaviour
{
    [SerializeField] private float _speed = 0.15f;
    private PongPlayer _assignedPlayer;

    private Rigidbody _rb;

    public override void OnStartServer()
    {
        _rb = GetComponent<Rigidbody>();
    }

    [Server]
    public void MoveRacket(Vector3 direction)
    {
        transform.position += direction * _speed * Time.deltaTime;
    }
    
    [Server]
    public void AssignPlayer(PongPlayer player)
    {
        _assignedPlayer = player;
        player.PlayerInput.OnInputDirection += MoveRacket;
    }

    [Server]
    public void DisconnectPlayer()
    {
        _assignedPlayer.PlayerInput.OnInputDirection -= MoveRacket;
    }
}
