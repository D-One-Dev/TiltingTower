using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActiveBlocksArray : IInitializable
{
    private List<Rigidbody2D> activeBlocks;

    public void Initialize()
    {
        activeBlocks = new List<Rigidbody2D>();
    }

    public void AddNewBlock(Rigidbody2D block)
    {
        activeBlocks.Add(block);
    }

    public void FixBlocks()
    {
        foreach (Rigidbody2D block in activeBlocks)
        {
            block.bodyType = RigidbodyType2D.Static;
            block.gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
        }

        activeBlocks.Clear();
    }
}
