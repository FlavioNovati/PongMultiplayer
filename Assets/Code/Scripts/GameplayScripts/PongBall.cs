using UnityEngine;
using UnityEngine.Events;

using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class PongBall : MonoBehaviour
{
    [SerializeField] private float _startingSpeed = 1f;
    [SerializeField] private float _maxLinearSpeed = 15f;
    [SerializeField] private float _speedMultiplierOnCollision = 0.5f;

    private Rigidbody _rb;
    private Vector3 _linearVelocity;
    private bool _enabled = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _enabled = false;
    }
    
    public void StartBall()
    {
        _enabled = true;
        Vector3 startingDir = UnityEngine.Random.value > 0.5f ? Vector3.left : Vector3.right;
        _rb.linearVelocity = startingDir * _startingSpeed;
    }

    public void StopBall()
    {
        _enabled = false;
        _linearVelocity = Vector3.zero;
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public void Reset()
    {
        StopBall();
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (!_enabled)
            return;

        if(_rb.linearVelocity != Vector3.zero)
            _linearVelocity = _rb.linearVelocity;

        //Clamp max linear velocity
        _rb.maxLinearVelocity = _maxLinearSpeed;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //Get linear velocity
        Vector3 linearVelocity = _linearVelocity;
        //Get average collision normal
        Vector3 collisionNormal = collision.GetCollisionNormal();        
        //Reflect linear velocity with normal
        Vector3 newLinearVelocity = Vector3.Reflect(_linearVelocity, collisionNormal);

        //Increase linear velocity
        Vector3 linearVelocityIncrease = newLinearVelocity * _speedMultiplierOnCollision;
        _rb.linearVelocity = newLinearVelocity + linearVelocityIncrease;
    }
}
