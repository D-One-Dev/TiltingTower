using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public event Action<GameObject> onBlockCollision;

    public void BlockCollision(GameObject gameObject)
    {
        onBlockCollision?.Invoke(gameObject);
    }
}
