using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private int _minNumberRandomRange = 2;
    private int _maxNumberRandomRange = 7;
    private int _magnitudeUpwardForce = 10;
    private Rigidbody _newCubeRigidbody;

    public void SpawnCubes(Vector3 position, Vector3 scale, float currentChance, GameObject _cubePrefab)
    {
        int cubeCount = Random.Range(_minNumberRandomRange, _maxNumberRandomRange);
        int numberDecreaseChance = 2;

        for (int i = 0; i < cubeCount; i++)
        {
            GameObject newCube = Instantiate(_cubePrefab, position, Quaternion.identity);
            newCube.transform.localScale = scale;

            Exlode(newCube);

            if (newCube.TryGetComponent(out CubeExplosions cubeScript))
            {
                cubeScript.CurrentChance = currentChance / numberDecreaseChance;
            }
        }
    }

    private void Exlode(GameObject Cube)
    {
        _newCubeRigidbody = Cube.GetComponent<Rigidbody>();
        _newCubeRigidbody.AddForce(Vector3.up * _magnitudeUpwardForce, ForceMode.Impulse);
    }
}
