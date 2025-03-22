using UnityEngine;
using Zenject;

public class BlockSpawner : IInitializable
{
    [Inject(Id = "BlockSpawnPoint")]
    private readonly Transform _blockSpawnPoint;
    [Inject(Id = "BlockPrefabs")]
    private readonly GameObject[] _blockPrefabs;
    [Inject(Id = "BlocksParent")]
    private readonly Transform _blocksParent;

    private DiContainer _container;
    private EventHandler _evevntHandler;

    [Inject]
    public void Construct(DiContainer container, EventHandler eventHandler)
    {
        _container = container;
        _evevntHandler = eventHandler;
        _evevntHandler.OnSpawnBlock += SpawnBlock;
    }

    public void Initialize()
    {
        SpawnBlock();
    }

    public void SpawnBlock()
    {
        GameObject blockPrefab = _blockPrefabs[Random.Range(0, _blockPrefabs.Length)];

        GameObject block = _container.InstantiatePrefab(blockPrefab, _blockSpawnPoint.position, Quaternion.identity, _blocksParent);
        if (block.TryGetComponent(out Rigidbody2D rb))
        {
            _evevntHandler.SetCurrentBlock(block, rb);
            return;
        }
        Debug.LogErrorFormat("Block " + block.name + " has no rigidbody2D");
    }
}
