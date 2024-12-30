using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int _minNumberRandomRange = 2;
    private int _maxNumberRandomRange = 6;

    public void SpawnCubes(Vector3 position, Vector3 scale, float currentChance, Cube _cubePrefab)
    {
        int cubeCount = Random.Range(_minNumberRandomRange, _maxNumberRandomRange+1);
        int numberDecreaseChance = 2;

        for (int i = 0; i < cubeCount; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, position, Quaternion.identity);
            newCube.transform.localScale = scale;

            if (newCube.TryGetComponent(out Cube cubeScript))
            {
                cubeScript.CurrentChance = currentChance / numberDecreaseChance;
            }
        }
    }
}
