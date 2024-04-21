using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 1000;
    [SerializeField] private float _explosionRadius = 20;
    [SerializeField] private float _explosionForceReducer = 2;
    [SerializeField] private float _explosionRadiusReducer = 2;

    public float ExplosionForce => _explosionForce;

    public float ExplosionRadius => _explosionRadius;

    public void ReduceExplosionForce(float force)
    {
        _explosionForce = force / _explosionForceReducer;
    }

    public void ReduceExplosionRadius(float radius)
    {
        _explosionRadius = radius / _explosionRadiusReducer;
    }

    public void Explode()
    {
        foreach (Rigidbody blocks in GetExploadebleObjects())
            blocks.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExploadebleObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> blocks = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                blocks.Add(hit.attachedRigidbody);

        return blocks;
    }
}