using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Spawner _cubeManager;
    [SerializeField] private float _explosionRadius;

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
                    Vector3 spawnPosition1 = transform.position;
                    Vector3 newCubeScale = transform.localScale/2;

                    if (Random.value<=CurrentChance)
                    {
                        _cubeManager.SpawnCubes(spawnPosition1, newCubeScale, CurrentChance, _cubePrefab);
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}