using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 500;
    [SerializeField] private float _explosionRadius = 10;
    [SerializeField] private float _explosionForceMultiplier = 2;
    [SerializeField] private float _explosionRadiusMultiplier = 2;

    public float ExplosionForce => _explosionForce;

    public float ExplosionRadius => _explosionRadius;

    public void ReduceExplosionForce(float force)
    {
        _explosionForce = force * _explosionForceMultiplier;
    }

    public void ReduceExplosionRadius(float radius)
    {
        _explosionRadius = radius * _explosionRadiusMultiplier;
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