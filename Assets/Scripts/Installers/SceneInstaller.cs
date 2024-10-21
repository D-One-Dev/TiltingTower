using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Transform blockSpawnPoint;
    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private float blockFallSpeed;
    [SerializeField] private float minBlockHorizontalMoveDelta;
    [SerializeField] private float blockHorizontalMoveAmount;
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
            .AsSingle()
            .NonLazy();
        this.Container.Bind<ActiveBlocksArray>()
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

        this.Container.BindInterfacesAndSelfTo<BlockMoveInput>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}