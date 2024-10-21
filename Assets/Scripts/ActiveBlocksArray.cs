using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActiveBlocksArray
{
    private List<Rigidbody2D> activeBlocks;

    [Inject]
    public void Counstruct()
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
        }

        activeBlocks.Clear();
    }
}
