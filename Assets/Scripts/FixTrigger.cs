using UnityEngine;

public class FixTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _blockLayerMask;
    [SerializeField] private float nextTriggerVerticalDelta;
    public bool CheckCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) - Vector2.right * 15,
            Vector2.right, 30, _blockLayerMask);
        if (hit)
        {
            transform.position += Vector3.up * nextTriggerVerticalDelta;
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position - Vector3.right * 15, transform.position + Vector3.right * 15);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position - Vector3.right * 15 + Vector3.up * nextTriggerVerticalDelta,
            transform.position + Vector3.right * 15 + Vector3.up * nextTriggerVerticalDelta);
    }
}
