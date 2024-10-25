using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Transform blockSpawnPoint;
    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private float blockFallSpeed;
    [SerializeField] private float minBlockHorizontalMoveDelta;
    [SerializeField] private float blockHorizontalMoveAmount;
    [SerializeField] private float blockVerticalMoveAmount;
    [SerializeField] private FixTrigger[] fixTriggers;
    [SerializeField] private float cameraMovementSmoothness;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform blocksParent;
    [SerializeField] private LayerMask blocksLayer;
    [SerializeField] private float nextTriggerVerticalOffset;
    [SerializeField] private Transform canvasBottomPoint;
    public override void InstallBindings()
    {
        this.Container.Bind<Transform>()
            .WithId("BlockSpawnPoint")
            .FromInstance(blockSpawnPoint)
            .AsTransient();
        this.Container.Bind<GameObject[]>()
            .WithId("BlockPrefabs")
            .FromInstance(blockPrefabs)
            .AsTransient();
        this.Container.Bind<float>()
            .WithId("BlockFallSpeed")
            .FromInstance(blockFallSpeed)
            .AsTransient();
        this.Container.BindInterfacesAndSelfTo<BlockMover>()
            .FromNew()
            .AsSingle();
        this.Container.BindInterfacesAndSelfTo<BlockSpawner>()
            .FromNew()
            .AsSingle()
            .NonLazy();
        this.Container.BindInterfacesAndSelfTo<ActiveBlocksArray>()
            .FromNew()
            .AsSingle();

        this.Container.Bind<Controls>()
            .FromNew()
            .AsTransient();

        this.Container.Bind<float>()
            .WithId("MinBlockHorizontalMoveDelta")
            .FromInstance(minBlockHorizontalMoveDelta)
            .AsTransient();

        this.Container.Bind<float>()
            .WithId("BlockHorizontalMoveAmount")
            .FromInstance(blockHorizontalMoveAmount)
            .AsTransient();
        this.Container.Bind<float>()
            .WithId("BlockVerticalMoveAmount")
            .FromInstance(blockVerticalMoveAmount)
            .AsTransient();

        this.Container.BindInterfacesAndSelfTo<BlockMoveInput>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        this.Container.Bind<FixTrigger[]>()
            .WithId("FixTriggers")
            .FromInstance(fixTriggers)
            .AsTransient();

        this.Container.Bind<float>()
            .WithId("CameraMovementSmoothness")
            .FromInstance(cameraMovementSmoothness)
            .AsTransient();
        this.Container.Bind<Transform>()
            .WithId("Camera")
            .FromInstance(cam)
            .AsTransient();

        this.Container.BindInterfacesAndSelfTo<CameraMovement>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        this.Container.Bind<Transform>()
            .WithId("BlocksParent")
            .FromInstance(blocksParent)
            .AsTransient();

        this.Container.BindInterfacesAndSelfTo<FPSUnlocker>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        this.Container.Bind<LayerMask>()
            .WithId("BlocksLayer")
            .FromInstance(blocksLayer)
            .AsTransient();
        this.Container.Bind<float>()
            .WithId("NextTriggerVerticalOffset")
            .FromInstance(nextTriggerVerticalOffset)
            .AsTransient();

        this.Container.Bind<Transform>()
            .WithId("CanvasBottomPoint")
            .FromInstance(canvasBottomPoint)
            .AsTransient();
    }
}