using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private int playerHealth;
    [SerializeField] private Image helathbar;
    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private float verticalAccelerationScale;
    public override void InstallBindings()
    {
        Container.Bind<Transform>()
            .WithId("BlockSpawnPoint")
            .FromInstance(blockSpawnPoint)
            .AsTransient();

        Container.Bind<GameObject[]>()
            .WithId("BlockPrefabs")
            .FromInstance(blockPrefabs)
            .AsTransient();

        Container.Bind<float>()
            .WithId("BlockFallSpeed")
            .FromInstance(blockFallSpeed)
            .AsTransient();

        Container.BindInterfacesAndSelfTo<BlockMover>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<BlockSpawner>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<ActiveBlocksArray>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<Controls>()
            .FromNew()
            .AsTransient();

        Container.Bind<float>()
            .WithId("MinBlockHorizontalMoveDelta")
            .FromInstance(minBlockHorizontalMoveDelta)
            .AsTransient();

        Container.Bind<float>()
            .WithId("BlockHorizontalMoveAmount")
            .FromInstance(blockHorizontalMoveAmount)
            .AsTransient();

        Container.Bind<float>()
            .WithId("BlockVerticalMoveAmount")
            .FromInstance(blockVerticalMoveAmount)
            .AsTransient();

        Container.BindInterfacesAndSelfTo<BlockMoveInput>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<FixTrigger[]>()
            .WithId("FixTriggers")
            .FromInstance(fixTriggers)
            .AsTransient();

        Container.Bind<float>()
            .WithId("CameraMovementSmoothness")
            .FromInstance(cameraMovementSmoothness)
            .AsTransient();

        Container.Bind<Transform>()
            .WithId("Camera")
            .FromInstance(cam)
            .AsTransient();

        Container.BindInterfacesAndSelfTo<CameraMovement>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<Transform>()
            .WithId("BlocksParent")
            .FromInstance(blocksParent)
            .AsTransient();

        Container.BindInterfacesAndSelfTo<FPSUnlocker>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<LayerMask>()
            .WithId("BlocksLayer")
            .FromInstance(blocksLayer)
            .AsTransient();

        Container.Bind<float>()
            .WithId("NextTriggerVerticalOffset")
            .FromInstance(nextTriggerVerticalOffset)
            .AsTransient();

        Container.Bind<Transform>()
            .WithId("CanvasBottomPoint")
            .FromInstance(canvasBottomPoint)
            .AsTransient();

        Container.Bind<int>()
            .WithId("PlayerHealth")
            .FromInstance(playerHealth)
            .AsTransient();

        Container.Bind<Image>()
            .WithId("Healthbar")
            .FromInstance(helathbar)
            .AsTransient();

        Container.BindInterfacesAndSelfTo<PlayerHealth>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<EventHandler>()
            .FromInstance(eventHandler)
            .AsSingle()
            .NonLazy();

        Container.Bind<float>()
        .WithId("VerticalAccelerationScale")
        .FromInstance(verticalAccelerationScale)
        .AsTransient();
    }
}