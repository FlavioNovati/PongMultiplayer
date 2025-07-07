using Mirror;
using System;
using UnityEngine;

public class TableBorder : NetworkBehaviour
{
    public event Action OnBallCollision;

    [Server]
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PongBall>() != null)
            OnBallCollision?.Invoke();
    }
}
