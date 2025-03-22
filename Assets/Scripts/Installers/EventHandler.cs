using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public event Action<Rigidbody2D> OnAddBlockToActiveBlocksArray;
    public event Action<Rigidbody2D> OnRemoveBlockFromActiveBlocksArray;
    public event Action OnFixBlocksInActiveBlocksArray;
    public event Action<GameObject> OnBlockFallCollision;
    public event Action<int> OnMoveBlockHorizontal;
    public event Action OnRotateBlock;
    public event Action<float> OnMoveBlockVertical;
    public event Action OnSpawnBlock;
    public event Action<GameObject> OnUpdateMaxBlockHeight;
    public event Action<GameObject> OnUpdateMaxHeight;
    public event Action<GameObject, Rigidbody2D> OnSetCurrentBlock;
    public event Action OnBlockFell;

    public void AddBlockToActiveBlocksArray(Rigidbody2D block)
    {
        OnAddBlockToActiveBlocksArray?.Invoke(block);
    }
    public void RemoveBlockFromActiveBlocksArray(Rigidbody2D block)
    {
        OnRemoveBlockFromActiveBlocksArray?.Invoke(block);
    }
    public void FixBlocksInActiveBlocksArray()
    {
        OnFixBlocksInActiveBlocksArray?.Invoke();
    }
    public void BlockFallCollision(GameObject block)
    {
        OnBlockFallCollision?.Invoke(block);
    }
    public void MoveBlockHorizontal(int direction)
    {
        OnMoveBlockHorizontal?.Invoke(direction);
    }
    public void RotateBlock()
    {
        OnRotateBlock?.Invoke();
    }
    public void MoveBlockVertical(float amount)
    {
        OnMoveBlockVertical?.Invoke(amount);
    }
    public void SpawnBlock()
    {
        OnSpawnBlock?.Invoke();
    }
    public void UpdateMaxBlockHeight(GameObject block)
    {
        OnUpdateMaxBlockHeight?.Invoke(block);
    }
    public void UpdateMaxHeight(GameObject block)
    {
        OnUpdateMaxHeight?.Invoke(block);
    }
    public void SetCurrentBlock(GameObject block, Rigidbody2D blockRb)
    {
        OnSetCurrentBlock?.Invoke(block, blockRb);
    }
    public void BlockFell()
    {
        OnBlockFell?.Invoke();
    }
}
