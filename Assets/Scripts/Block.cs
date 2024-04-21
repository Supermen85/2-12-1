using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(Explosion))]

public class Block : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    [SerializeField] private float _chanceToCreatePercent = 100;
    [SerializeField] private float _chanceReducer = 2;
    [SerializeField] private float _scaleReducer = 2;

    public float ChanceToCreatePercent => _chanceToCreatePercent;
   
    public void ReduceCreateChance(float chance)
    {
        _chanceToCreatePercent = chance / _chanceReducer;
    }

    public void ReduceScale(Vector3 scale)
    {
        transform.localScale = scale / _scaleReducer;
    }

    public void SetMaterial()
    {
        GetComponent<MeshRenderer>().material = _materials[Random.Range(0, _materials.Length)];
    }

    private void OnDisable()
    {
        GetComponent<Explosion>().Explode();
    }
}
