using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int _magnitudeUpwardForce = 10;

    public void Exlode(Cube cube)
    {
        if(cube.TryGetComponent<Rigidbody>(out Rigidbody newCubeRigidbody))
        {
            newCubeRigidbody.AddForce(Vector3.up * _magnitudeUpwardForce, ForceMode.Impulse);
        }
    }
}
