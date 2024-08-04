using UnityEngine;

[RequireComponent(typeof(Exploder))]

public class CubeSplitter : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _minAmount = 2;
    [SerializeField] private int _maxAmount = 6;
    [SerializeField] private float _scaleDivisor = 2f;
    [SerializeField] private float _splitChanceDivisor = 2f;
    [SerializeField] private float ExplodeCoefficient = .1f;

    private const float Angle = 360f;
    private const float BasicExplodeCoefficient = 1f;

    private Cube _newCube;
    private Exploder _exploder;
    private Collider[] _newCubes;
    private int _cubesAmount;
    private float _newCubeSplitChance;
    private float _newCubesSpacing;
    private float _radius;
    private float _explodeForceDivisor;
    private float _explodeRadiusDivisor;
    private Vector3 _circularPosition;
    private Vector3 _newCubePosition;
    private Vector3 _explosionPosition;
    private Vector3 _newCubeScale;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _newCubesSpacing = Angle * Mathf.Deg2Rad;
    }

    public void Split(Cube cube)
    {
        _explosionPosition = cube.transform.position;

        if (cube.IsSplit() == false)
        {
            Destroy(cube.gameObject);

            _explodeForceDivisor = cube.transform.localScale.x * ExplodeCoefficient;
            _explodeRadiusDivisor = cube.transform.localScale.x * ExplodeCoefficient;

            _exploder.ExplodeTargets
                (_explodeForceDivisor, _explosionPosition, _explodeRadiusDivisor);

            return;
        }

        _newCubeScale = cube.transform.localScale / _scaleDivisor;
        _newCubeSplitChance = cube.SplitChance / _splitChanceDivisor;
        _radius = cube.transform.localScale.x / _scaleDivisor;
        _cubesAmount = Random.Range(_minAmount, _maxAmount + 1);
        _newCubes = new Collider[_cubesAmount];

        Destroy(cube.gameObject);

        for (int i = 1; i <= _cubesAmount; i++)
        {
            _circularPosition = new(
                Mathf.Cos(_newCubesSpacing / _cubesAmount * i) * _radius,
                0,
                Mathf.Sin(_newCubesSpacing / _cubesAmount * i) * _radius);

            _newCubePosition = _circularPosition + _explosionPosition;

            _newCube = Instantiate(_prefab, _newCubePosition, Quaternion.identity);

            _newCube.SetScale(_newCubeScale);
            _newCube.SetSplitChance(_newCubeSplitChance);
            _newCubes[i - 1] = _newCube.Collider;
        }

        _exploder.ExplodeTargets(BasicExplodeCoefficient, _explosionPosition,
            BasicExplodeCoefficient, _newCubes);
    }
}