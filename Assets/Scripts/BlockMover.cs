using System;
using UnityEngine;
using Zenject;

public class BlockMover : ITickable
{
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

    private float _blockHorizontalDelta;
    private float _blockVerticalDelta;
    private EventHandler _eventHandler;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;

        _eventHandler.OnBlockFallCollision += FallCollision;
        _eventHandler.OnMoveBlockHorizontal += MoveBlockHorizontal;
        _eventHandler.OnRotateBlock += RotateBlock;
        _eventHandler.OnSetCurrentBlock += SetCurrentBlock;
        _eventHandler.OnMoveBlockVertical += MoveBlockVertical;
    }

    public void Tick()
    {
        if (_currentBlockRB != null)
        {
            _currentBlockRB.transform.Translate(_currentBlockRB.transform.InverseTransformDirection(Vector3.right) * _blockHorizontalDelta);
            _blockHorizontalDelta = 0f;

            Vector2 delta = Vector2.down * (_blockFallSpeed + _blockVerticalDelta);

            RaycastHit2D[] collisions = new RaycastHit2D[10];
            int hitCount = _currentBlockRB.Cast(delta, collisions);
            if (hitCount <= 0)
            {
                _currentBlockRB.MovePosition(_currentBlockRB.position + delta);
            }
            else
            {
                _currentBlockRB.MovePosition(_currentBlockRB.position - delta * 0.25f);
                BlockCollision();
            }
            _blockVerticalDelta = 0f;
        }
    }

    private void BlockCollision()
    {
        _currentBlockRB.linearVelocity = Vector3.zero;
        _currentBlockRB.angularVelocity = 0;
        _currentBlockRB.gravityScale = 1f;
        _currentBlockShader.DisableShade();

        _eventHandler.AddBlockToActiveBlocksArray(_currentBlockRB);

        foreach (FixTrigger fixTrigger in _fixTriggers)
        {
            if (fixTrigger.CheckCollision())
            {
                _eventHandler.FixBlocksInActiveBlocksArray();
                break;
            }
        }

        _eventHandler.UpdateMaxBlockHeight(_currentBlock);
        _eventHandler.UpdateMaxHeight(_currentBlock);
        _eventHandler.SpawnBlock();
    }

    public void FallCollision(GameObject block)
    {
        if (block == _currentBlock)
        {
            _eventHandler.SpawnBlock();
        }
        _eventHandler.AddBlockToActiveBlocksArray(block.GetComponent<Rigidbody2D>());
        Debug.Log("Block Fell!!!!!");
        _eventHandler.BlockFell();
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
        _blockVerticalDelta = -_blockVerticalMoveAmount * amount;
    }
}