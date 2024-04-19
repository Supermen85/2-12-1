using UnityEngine;

public class Creation : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Block _prefab;

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

        Destroy(block.gameObject);

        if (CanCreate(block))
            CreateBlocks(block);
    }

    private void CreateBlocks(Block block)
    {
        int count = GetCreatingBlocksCount();

        for (int i = 0; i < count; i++)
        {
            Block newBlock = Instantiate(_prefab, GetCreatePosition(block), Quaternion.identity);

            newBlock.SetMaterial();
            newBlock.ReduceScale(block.transform.localScale);
            newBlock.ReduceCreateChance(block.ChanceToCreatePercent);
            newBlock.Push(block.transform.position);
        }
    }

    private bool CanCreate(Block block)
    {
        int maxChance = 100;

        if (Random.Range(0, maxChance + 1) <= block.ChanceToCreatePercent)
            return true;

        return false;
    }

    private int GetCreatingBlocksCount()
    {
        int min = 2;
        int max = 6;

        return Random.Range(min, max + 1);
    }

    private Vector3 GetCreatePosition(Block block)
    {
        float deltaX = Random.Range(-10, 11) / 10f;
        float deltaZ = Random.Range(-10, 11) / 10f;

        Vector3 position = block.transform.position;

        position.x += deltaX;
        position.z += deltaZ;

        return position;
    }
}
