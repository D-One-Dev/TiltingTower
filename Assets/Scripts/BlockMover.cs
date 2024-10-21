using UnityEngine;
using Zenject;

public class BlockMover : IFixedTickable
{
    [Inject(Id = "BlockFallSpeed")]
    private readonly float _blockFallSpeed;
    [Inject(Id = "BlockHorizontalMoveAmount")]
    private readonly float _blockHorizontalMoveAmount;

    private GameObject _currentBlock;
    private Rigidbody2D _currentBlockRB;
    private ActiveBlocksArray _activeBlocksArray;
    private BlockSpawner _blockSpawner;

    private Vector3 _blockHorizontalDelta = Vector3.zero;

    [Inject]
    public void Construct(BlockSpawner blockSpawner, ActiveBlocksArray activeBlocksArray)
    {
        _blockSpawner = blockSpawner;
        _activeBlocksArray = activeBlocksArray;
    }

    public void FixedTick()
    {
        if (_currentBlockRB != null)
        {
            _currentBlockRB.MovePosition(_currentBlockRB.transform.position + (Vector3.down * _blockFallSpeed) + _blockHorizontalDelta);
            _blockHorizontalDelta = Vector3.zero;
        }
    }

    private void BlockCollision()
    {
        _activeBlocksArray.FixBlocks();
        _currentBlockRB.bodyType = RigidbodyType2D.Dynamic;
        _activeBlocksArray.AddNewBlock(_currentBlockRB);
        _blockSpawner.SpawnBlock();
    }

    public void SetCurrentBlock(GameObject block, Rigidbody2D rb)
    {
        if (_currentBlock != null && _currentBlock.TryGetComponent<BlockCollider>(out BlockCollider lastBlockCollider))
        {
            lastBlockCollider.OnBlockCollision -= BlockCollision;
        }
        _currentBlock = block;
        _currentBlockRB = rb;
        if(_currentBlock.TryGetComponent<BlockCollider>(out BlockCollider blockCollider))
        {
            blockCollider.OnBlockCollision += BlockCollision;
        }
    }

    public void MoveBlockHorizontal(int direction)
    {
        _blockHorizontalDelta = _blockHorizontalMoveAmount * direction * Vector3.right;
    }
}