using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActiveBlocksArray
{
    private readonly List<Rigidbody2D> _activeBlocks = new();

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        eventHandler.OnAddBlockToActiveBlocksArray += AddBlockToActiveBlocksArray;
        eventHandler.OnRemoveBlockFromActiveBlocksArray += RemoveBlockFromActiveBlocksArray;
        eventHandler.OnFixBlocksInActiveBlocksArray += FixBlocksInActiveBlocksArray;
    }

    public void AddBlockToActiveBlocksArray(Rigidbody2D block)
    {
        _activeBlocks.Add(block);
    }

    public void RemoveBlockFromActiveBlocksArray(Rigidbody2D rb)
    {
        if (_activeBlocks.Contains(rb)) _activeBlocks.Remove(rb);
    }

    public void FixBlocksInActiveBlocksArray()
    {
        foreach (Rigidbody2D block in _activeBlocks)
        {
            if (block != null)
            {
                block.gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                MonoBehaviour.Destroy(block);
            }
        }

        _activeBlocks.Clear();
    }
}