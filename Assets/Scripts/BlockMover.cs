using System;
using UnityEngine;
using Zenject;

public class BlockMover : ITickable
{
    public event Action OnBlockFell;

    [Inject(Id = "BlockFallSpeed")]
    private readonly float _blockFallSpeed;
    [Inject(Id = "BlockHorizontalMoveAmount")]
    private readonly float _blockHorizontalMoveAmount;
    [Inject(Id = "FixTriggers")]
    private readonly FixTrigger[] _fixTriggers;
    [Inject(Id = "BlockVerticalMoveAmount")]
    private readonly float _blockVerticalMoveAmount;

    private GameObject _currentBlock;
    private Rigidbody2D _currentBlockRB;
    private BlockShader _currentBlockShader;
    private ActiveBlocksArray _activeBlocksArray;
    private BlockSpawner _blockSpawner;
    private CameraMovement _cameraMovement;
    private MaxHeightCounter _maxHeightCounter;

    private float _blockHorizontalDelta;
    private float _blockVerticalDelta;

    [Inject]
    public void Construct(BlockSpawner blockSpawner, ActiveBlocksArray activeBlocksArray, CameraMovement cameraMovement,
        MaxHeightCounter maxHeightCounter)
    {
        _blockSpawner = blockSpawner;
        _activeBlocksArray = activeBlocksArray;
        _cameraMovement = cameraMovement;
        _maxHeightCounter = maxHeightCounter;

        BlockCollider.OnFallCollision += FallCollision;
    }

    public void Tick()
    {
        if (_currentBlockRB != null)
        {
            _currentBlockRB.transform.Translate(_currentBlockRB.transform.InverseTransformDirection(Vector3.right) * _blockHorizontalDelta);
            _blockHorizontalDelta = 0f;

            RaycastHit2D[] collisions = new RaycastHit2D[10];
            int hitCount = _currentBlockRB.Cast(Vector2.down * (_blockFallSpeed + _blockVerticalDelta), collisions);
            if (hitCount <= 0)
            {
                _currentBlockRB.linearVelocity = Vector2.down * (_blockFallSpeed + _blockVerticalDelta);
            }
            else
            {
                BlockCollision();
            }
            _blockVerticalDelta = 0f;
        }
    }

    private void BlockCollision()
    {
        bool flag = false;
        foreach (FixTrigger fixTrigger in _fixTriggers)
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
            _currentBlockRB.linearVelocity = Vector3.zero;
            _currentBlockRB.angularVelocity = 0;
            _currentBlockRB.gravityScale = 1f;

            _currentBlockShader.DisableShade();

            _activeBlocksArray.AddNewBlock(_currentBlockRB);
        }
        _cameraMovement.UpdateMaxBlockHeight(_currentBlock);
        _maxHeightCounter.UpdateMaxHeight(_currentBlock);
        _blockSpawner.SpawnBlock();
    }

    public void FallCollision(GameObject block)
    {
        if (block == _currentBlock)
        {
            _blockSpawner.SpawnBlock();
        }
        _activeBlocksArray.RemoveBlock(block.GetComponent<Rigidbody2D>());
        Debug.Log("Block Fell!!!!!");
        OnBlockFell?.Invoke();
    }

    public void SetCurrentBlock(GameObject block, Rigidbody2D rb)
    {
        _currentBlock = block;
        _currentBlockRB = rb;
        _currentBlockShader = _currentBlock.GetComponent<BlockShader>();
        _currentBlockShader.UpdateShade(0f);
    }

    public void MoveBlockHorizontal(int direction)
    {
        _blockHorizontalDelta = _blockHorizontalMoveAmount * direction;
    }

    public void RotateBlock()
    {
        _currentBlock.transform.Rotate(0f, 0f, -90f);
        _currentBlockShader.UpdateShade(_currentBlock.transform.localEulerAngles.z);
    }

    public void MoveBlockVertical(float amount)
    {
        Debug.Log(amount);
        _blockVerticalDelta = -_blockVerticalMoveAmount * amount;
    }
}