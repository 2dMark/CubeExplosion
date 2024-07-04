using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _container;

    private ObjectPool<Cube> _pool;
    private int _defaultGeneration = 0;

    public Cube Prefab => _prefab;

    private void Awake()
    {
        _pool = new(_prefab, _container);
    }

    public Cube Get()
    {
        Cube cube = _pool.GetObject();

        cube.SetGeneration(_defaultGeneration);
        cube.gameObject.SetActive(true);

        return cube;
    }

    public Cube Get(int generation)
    {
        Cube cube = _pool.GetObject();

        cube.SetGeneration(generation);
        cube.gameObject.SetActive(true);

        return cube;
    }

    public void Put(Cube cube) => _pool.PutObject(cube);
}