using UnityEngine;

public class Creation : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Block _prefab;
    [SerializeField] private int _creatingBlocksMin = 2;
    [SerializeField] private int _creatingBlocksMax = 6;

    private RaycastHit _hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == false)
            return;

        Ray raycast = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(raycast, out _hit) == false)
            return;

        if (_hit.transform.TryGetComponent<Block>(out Block block) == false)
            return;

        if (CanCreate(block))
            CreateBlocks(block);

        Destroy(block.gameObject);
    }

    private void CreateBlocks(Block block)
    {
        int count = GetCreatingBlocksCount();

        for (int i = 0; i < count; i++)
        {
            Block newBlock = Instantiate(_prefab, GetCreatePosition(block), Quaternion.identity);

            Explosion explosion = block.GetComponent<Explosion>();

            newBlock.SetMaterial();
            newBlock.ReduceScale(block.transform.localScale);
            newBlock.ReduceCreateChance(block.ChanceToCreatePercent);

            Explosion newExplosion = newBlock.GetComponent<Explosion>();

            newExplosion.ReduceExplosionForce(explosion.ExplosionForce);
            newExplosion.ReduceExplosionRadius(explosion.ExplosionRadius);
        }
    }

    private bool CanCreate(Block block)
    {
        int maxChance = 100;

        return Random.Range(0, maxChance + 1) <= block.ChanceToCreatePercent;
    }

    private int GetCreatingBlocksCount()
    {
        return Random.Range(_creatingBlocksMin, _creatingBlocksMax + 1);
    }

    private Vector3 GetCreatePosition(Block block)
    {
        int min = -10;
        int max = 11;
        float divisor = 10f;

        float deltaX = Random.Range(min, max) / divisor;
        float deltaZ = Random.Range(min, max) / divisor;

        Vector3 position = block.transform.position;

        position.x += deltaX;
        position.z += deltaZ;

        return position;
    }
}
