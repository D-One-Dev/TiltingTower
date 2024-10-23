using UnityEngine;
using Zenject;

public class BlockMoveInput : ITickable
{
    [Inject(Id = "MinBlockHorizontalMoveDelta")]
    private readonly float minBlockHorizontalMoveDelta;

    private BlockMover _blockMover;
    private Controls _controls;

    private bool _primaryTouch;
    private Vector2 _lastTouchPosition;

    [Inject]
    public void Construct(Controls controls, BlockMover blockMover)
    {
        _blockMover = blockMover;
        _controls = controls;

        _controls.Gameplay.PointerTap.started += ctx =>
        {
            _primaryTouch = true;
            _lastTouchPosition = _controls.Gameplay.PointerPos.ReadValue<Vector2>();
        };
        _controls.Gameplay.PointerTap.canceled += ctx =>
        {
            _primaryTouch = false;
            if(_controls.Gameplay.PointerPos.ReadValue<Vector2>() - _lastTouchPosition == Vector2.zero) _blockMover.RotateBlock();
        };

        _controls.Enable();
    }

    public void Tick()
    {
        if (_primaryTouch)
        {
            Vector2 currentTouchPosition = _controls.Gameplay.PointerPos.ReadValue<Vector2>();
            Vector2 pointerDelta = currentTouchPosition - _lastTouchPosition;

            if(pointerDelta.x > minBlockHorizontalMoveDelta)
            {
                _blockMover.MoveBlockHorizontal(1);

                _lastTouchPosition = currentTouchPosition;
            }

            else if (pointerDelta.x < -minBlockHorizontalMoveDelta)
            {
                _blockMover.MoveBlockHorizontal(-1);

                _lastTouchPosition = currentTouchPosition;
            }
        }
    }
}