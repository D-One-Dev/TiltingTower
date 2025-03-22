using UnityEngine;
using Zenject;

public class BlockMoveInput : ITickable
{
    [Inject(Id = "MinBlockHorizontalMoveDelta")]
    private readonly float _minBlockHorizontalMoveDelta;

    [Inject(Id = "VerticalAccelerationScale")]
    private readonly float _verticalAccelerationScale;
    private Controls _controls;

    private bool _primaryTouch;
    private Vector2 _lastTouchPosition;
    private Vector2 _lastTouchStartPosition;
    private EventHandler _eventHandler;

    [Inject]
    public void Construct(Controls controls, EventHandler eventHandler)
    {
        _controls = controls;
        _eventHandler = eventHandler;

        //debug keyboard input
        _controls.Gameplay.MoveBlockLeft.performed += ctx => _eventHandler.MoveBlockHorizontal(-1);
        _controls.Gameplay.MoveBlockRight.performed += ctx => _eventHandler.MoveBlockHorizontal(1);
        _controls.Gameplay.RotateBlock.performed += ctx => _eventHandler.RotateBlock();

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
            if (delta.magnitude < (Vector2.one * 50f).magnitude) _eventHandler.RotateBlock();
        };

        _controls.Enable();
    }

    public void Tick()
    {
        if (_primaryTouch)
        {
            Vector2 currentTouchPosition = _controls.Gameplay.PointerPos.ReadValue<Vector2>();
            Vector2 pointerDelta = currentTouchPosition - _lastTouchPosition;

            if (pointerDelta.x > _minBlockHorizontalMoveDelta)
            {
                _eventHandler.MoveBlockHorizontal(1);

                _lastTouchPosition = currentTouchPosition;
            }

            else if (pointerDelta.x < -_minBlockHorizontalMoveDelta)
            {
                _eventHandler.MoveBlockHorizontal(-1);

                _lastTouchPosition = currentTouchPosition;
            }

            if (pointerDelta.y < 0) _eventHandler.MoveBlockVertical(pointerDelta.y * _verticalAccelerationScale);
        }
    }
}