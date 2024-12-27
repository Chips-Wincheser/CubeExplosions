using UnityEngine;

public class CubeExplosions : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private CubeManager _cubeManager;
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
