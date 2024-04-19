using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(MeshRenderer))]

public class Block : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private float _chanceToCreatePercent = 100;

    public float ChanceToCreatePercent => _chanceToCreatePercent;

    public void ReduceCreateChance(float chance)
    {
        int coefficient = 2;

        _chanceToCreatePercent = chance / coefficient;
    }

    public void Push(Vector3 center)
    {
        float force = 10f;

        Vector3 direction = (transform.position - center).normalized;

        GetComponent<Rigidbody>().velocity = direction * force;
    }

    public void ReduceScale(Vector3 scale)
    {
        int coefficient = 2;

        transform.localScale = scale / coefficient;
    }

    public void SetMaterial()
    {
        GetComponent<MeshRenderer>().material = _materials[Random.Range(0, _materials.Length)];
    }
}