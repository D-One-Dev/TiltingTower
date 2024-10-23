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

    private BlockMover _blockMover;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container, BlockMover blockMover)
    {
        _container = container;
        _blockMover = blockMover;
    }

    public void Initialize()
    {
        SpawnBlock();
    }

    public void SpawnBlock()
    {
        GameObject blockPrefab = _blockPrefabs[Random.Range(0, _blockPrefabs.Length)];

        GameObject block = _container.InstantiatePrefab(blockPrefab, _blockSpawnPoint.position, Quaternion.identity, _blocksParent);
        if(block.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            _blockMover.SetCurrentBlock(block, rb);
            return;
        }
        Debug.LogErrorFormat("Block " + block.name + " has no rigidbody2D");
    }
}
