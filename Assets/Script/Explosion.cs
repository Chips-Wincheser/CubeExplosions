using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _radiusExplode;
    private float _explodeForce;

    public void Explode(float baseRadius, float baseForce, float cubeScaleFactor)
    {
        _radiusExplode = baseRadius / cubeScaleFactor;
        _explodeForce = baseForce / cubeScaleFactor;

        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explodeForce, transform.position, _radiusExplode);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radiusExplode);
        
        List<Rigidbody> barrels = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody!=null)
            {
                barrels.Add(hit.attachedRigidbody);
            }
        }

        return barrels;
    }
}
