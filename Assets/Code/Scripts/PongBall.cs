using UnityEngine;
using UnityEngine.Events;

using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class PongBall : MonoBehaviour
{
    [SerializeField] private float _startingSpeed = 1f;
    [SerializeField] private float _maxLinearSpeed = 15f;
    [SerializeField] private float _speedMultiplierOnCollision = 0.5f;
    
    [SerializeField] private UnityEvent OnCollision;

    private Rigidbody _rb;
    private Vector3 _linearVelocity;

    //[Server]
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    //[Server]
    private void OnEnable()
    {
        Vector3 startingDir = UnityEngine.Random.value > 0.5f? Vector3.left: Vector3.right;
        _rb.linearVelocity = startingDir * _startingSpeed;
    }

    //[Server]
    private void Update()
    {
        if(_rb.linearVelocity != Vector3.zero)
            _linearVelocity = _rb.linearVelocity;

        _rb.maxLinearVelocity = _maxLinearSpeed;
        Debug.Log(_rb.linearVelocity.magnitude);
    }

    //[Server]
    private void OnCollisionEnter(Collision collision)
    {
        //Get linear velocity
        Vector3 linearVelocity = _linearVelocity;
        //Get average collision normal
        Vector3 collisionNormal = collision.GetCollisionNormal();        
        //Reflect linear velocity with normal
        Vector3 newLinearVelocity = Vector3.Reflect(_linearVelocity, collisionNormal);


        Vector3 linearVelocityIncrease = newLinearVelocity * _speedMultiplierOnCollision;
        _rb.linearVelocity = newLinearVelocity + linearVelocityIncrease;

        OnCollision?.Invoke();
    }
}
