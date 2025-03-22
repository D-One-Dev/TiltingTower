using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D triggerCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (triggerCollider)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up * triggerCollider.size.y / 2 - Vector3.right * 15,
                transform.position + Vector3.up * triggerCollider.size.y / 2 + Vector3.right * 15);
        }
    }
}
