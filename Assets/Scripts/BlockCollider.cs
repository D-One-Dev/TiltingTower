using System;
using UnityEngine;
using Zenject;

public class BlockCollider : MonoBehaviour
{
    public static event Action<GameObject> OnBlockCollision;
    public static event Action<GameObject> OnFallCollision;

    private Rigidbody2D _rb;
    private EventHandler _eventHandler;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FallTrigger"))
        {
            if (_rb.bodyType == RigidbodyType2D.Kinematic || _rb.linearVelocity.magnitude > new Vector2(1f, 1f).magnitude)
            {
                OnFallCollision?.Invoke(this.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //OnBlockCollision?.Invoke(this.gameObject);
        //_eventHandler.BlockCollision(this.gameObject);
    }
}