using System;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public static event Action<GameObject> OnBlockCollision;
    public static event Action<GameObject> OnFallCollision;

    private Rigidbody2D _rb;

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
        OnBlockCollision?.Invoke(this.gameObject);
    }
}