using UnityEngine;
using Zenject;

public class BlockMoveInput : ITickable
{
    [Inject(Id = "MinBlockHorizontalMoveDelta")]
    private readonly float _minBlockHorizontalMoveDelta;

    [Inject(Id = "VerticalAccelerationScale")]
    private readonly float _verticalAccelerationScale;

    private BlockMover _blockMover;
    private Controls _controls;

    private bool _primaryTouch;
    private Vector2 _lastTouchPosition;
    private Vector2 _lastTouchStartPosition;

    [Inject]
    public void Construct(Controls controls, BlockMover blockMover)
    {
        _blockMover = blockMover;
        _controls = controls;

        //debug keyboard input
        _controls.Gameplay.MoveBlockLeft.performed += ctx => _blockMover.MoveBlockHorizontal(-1);
        _controls.Gameplay.MoveBlockRight.performed += ctx => _blockMover.MoveBlockHorizontal(1);
        _controls.Gameplay.RotateBlock.performed += ctx => _blockMover.RotateBlock();

        _controls.Gameplay.PointerTap.started += ctx =>
        {
            _primaryTouch = true;
            _lastTouchPosition = _controls.Gameplay.PointerPos.ReadValue<Vector2>();
            _lastTouchStartPosition = _lastTouchPosition;
        };
        _controls.Gameplay.PointerTap.canceled += ctx =>
        {
            _primaryTouch = false;
            Vector2 delta = _controls.Gameplay.PointerPos.ReadValue<Vector2>() - _lastTouchStartPosition;
            if (delta.magnitude < (Vector2.one * 50f).magnitude) _blockMover.RotateBlock();
        };

        _controls.Enable();
    }

    public void Tick()
    {
        if (_primaryTouch)
        {
            Vector2 currentTouchPosition = _controls.Gameplay.PointerPos.ReadValue<Vector2>();
            Vector2 pointerDelta = currentTouchPosition - _lastTouchPosition;

            if(pointerDelta.x > _minBlockHorizontalMoveDelta)
            {
                _blockMover.MoveBlockHorizontal(1);

                _lastTouchPosition = currentTouchPosition;
            }

            else if (pointerDelta.x < -_minBlockHorizontalMoveDelta)
            {
                _blockMover.MoveBlockHorizontal(-1);

                _lastTouchPosition = currentTouchPosition;
            }

            if (pointerDelta.y < 0) _blockMover.MoveBlockVertical(pointerDelta.y * _verticalAccelerationScale);
        }
    }
}