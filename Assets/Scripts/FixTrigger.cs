using UnityEngine;
using Zenject;

public class FixTrigger : MonoBehaviour
{
    [Inject(Id = "BlocksLayer")]
    private readonly LayerMask _blocksLayer;
    [Inject(Id = "NextTriggerVerticalOffset")]
    private readonly float _nextTriggerVerticalOffset;

    public bool CheckCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) - Vector2.right * 15,
            Vector2.right, 30, _blocksLayer);
        if (hit)
        {
            transform.position += Vector3.up * _nextTriggerVerticalOffset;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position - Vector3.right * 15, transform.position + Vector3.right * 15);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position - Vector3.right * 15 + Vector3.up * _nextTriggerVerticalOffset,
            transform.position + Vector3.right * 15 + Vector3.up * _nextTriggerVerticalOffset);
    }
}
