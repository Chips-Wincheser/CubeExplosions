using System.Collections.Generic;
using UnityEngine;

public class CubeExplosions : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private int _minNumberRandomRange=2;
    private int _maxNumberRandomRange=7;
    private float _currentChance = 1.0f;

    private float CurrentChance;

    private void Start()
    {
        if (CurrentChance<=0)
        {
            CurrentChance = _currentChance;
        }
    }

    private void OnMouseDown()
    {
        Vector3 spawnPosition1 = transform.position;
        Vector3 newCubScale = transform.localScale/2;
        int numberDecreaseChance = 2;

        if (Random.value<=CurrentChance)
        {
            for (int i = 0; i < Random.Range(_minNumberRandomRange, _maxNumberRandomRange); i++)
            {
                GameObject newCube = Instantiate(_cubePrefab, spawnPosition1, Quaternion.identity);
                newCube.transform.localScale = newCubScale;

                if (newCube.TryGetComponent(out CubeExplosions cubeScript))
                {
                    cubeScript.CurrentChance = CurrentChance / numberDecreaseChance;
                }

                Exlode();

                PaintCube(newCube);
            }
             
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PaintCube(GameObject cube)
    {
        Renderer _cubeRenderer = cube.GetComponent<Renderer>();
        _cubeRenderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void Exlode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> barrels = new ();

        foreach (Collider hit in hits)
        {
            if(hit.attachedArticulationBody!= null)
            {
                barrels.Add(hit.attachedRigidbody);
            }
        }

        return barrels;
    }
}
