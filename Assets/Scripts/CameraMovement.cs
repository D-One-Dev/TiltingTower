using UnityEngine;
using Zenject;

public class CameraMovement : ITickable
{
    [Inject(Id = "CameraMovementSmoothness")]
    private readonly float _cameraMovementSmoothness;
    [Inject(Id = "Camera")]
    private readonly Transform _camera;
    private float _maxBlockHeight = 0;

    public void Tick()
    {
        _camera.position = new Vector3(_camera.position.x,
            Mathf.Lerp(_camera.position.y, _maxBlockHeight, _cameraMovementSmoothness * Time.deltaTime),
            _camera.position.z);
    }

    public void UpdateMaxBlockHeight(GameObject block)
    {
        _maxBlockHeight = Mathf.Max(_maxBlockHeight, block.transform.position.y);
    }
}