using UnityEngine;
using Zenject;

public class BlockMover : IFixedTickable
{
    [Inject(Id = "BlockFallSpeed")]
    private readonly float _blockFallSpeed;
    [Inject(Id = "BlockHorizontalMoveAmount")]
    private readonly float _blockHorizontalMoveAmount;
    [Inject(Id = "FixTriggers")]
    private readonly FixTrigger[] _fixTriggers;

    private GameObject _currentBlock;
    private Rigidbody2D _currentBlockRB;
    private ActiveBlocksArray _activeBlocksArray;
    private BlockSpawner _blockSpawner;
    private CameraMovement _cameraMovement;

    private Vector3 _blockHorizontalDelta = Vector3.zero;

    [Inject]
    public void Construct(BlockSpawner blockSpawner, ActiveBlocksArray activeBlocksArray, CameraMovement cameraMovement)
    {
        _blockSpawner = blockSpawner;
        _activeBlocksArray = activeBlocksArray;
        _cameraMovement = cameraMovement;
    }

    public void FixedTick()
    {
        if (_currentBlockRB != null)
        {
            _currentBlockRB.MovePosition(_currentBlockRB.transform.position + (_blockFallSpeed * Vector3.down) + _blockHorizontalDelta);
            _blockHorizontalDelta = Vector3.zero;
        }
    }

    private void BlockCollision()
    {
        bool flag = false;
        foreach(FixTrigger fixTrigger in _fixTriggers)
        {
            if (fixTrigger.CheckCollision())
            {
                flag = true;
                break;
            }
        }

        if (flag)
        {
            _activeBlocksArray.AddNewBlock(_currentBlockRB);
            _activeBlocksArray.FixBlocks();
        }
        else
        {
            _currentBlockRB.bodyType = RigidbodyType2D.Dynamic;
            _activeBlocksArray.AddNewBlock(_currentBlockRB);
        }
        _cameraMovement.UpdateMaxBlockHeight(_currentBlock);
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

    public void RotateBlock()
    {
        _currentBlock.transform.Rotate(0f, 0f, -90f);
    }
}