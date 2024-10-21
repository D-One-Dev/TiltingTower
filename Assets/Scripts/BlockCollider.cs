using System;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public event Action OnBlockCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnBlockCollision?.Invoke();
    }
}