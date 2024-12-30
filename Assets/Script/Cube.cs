using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Explosion exception;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Spawner _cubeManager;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private Camera _camera;
    
    private RaycastHit _hit;
    private Ray _ray;

    private float _currentChance = 1.0f;
    public float CurrentChance;

    private void Awake()
    {
        Renderer _cubeRenderer = GetComponent<Renderer>();
        _cubeRenderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void Start()
    {
        if (CurrentChance<=0)
            CurrentChance = _currentChance;

        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
            {
                if(_hit.collider.gameObject == gameObject)
                {
                    int numberDivideScaleCub = 2;
                    Vector3 spawnPosition = transform.position;
                    Vector3 newCubeScale = transform.localScale/numberDivideScaleCub;

                    if (Random.value<=CurrentChance)
                    {
                        _cubeManager.SpawnCubes(spawnPosition, newCubeScale, CurrentChance, _cubePrefab);
                    }
                    else
                    {
                        float cubeScaleFactor = 1 / transform.localScale.magnitude;
                        exception.Explode(_explosionRadius, _explosionForce, cubeScaleFactor);
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}
