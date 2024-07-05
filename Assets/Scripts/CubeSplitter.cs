using UnityEngine;

public class CubeSplitter : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _minAmount = 2;
    [SerializeField] private int _maxAmount = 6;
    [SerializeField] private float _scaleDivider = 2f;
    [SerializeField] private float _SplitChanceDivider = 2f;
    [SerializeField] private float _explodeForce = 500f;
    [SerializeField] private float _explodeRadius = 50f;

    private int _cubesAmount;
    private float newCubeSplitChance;
    private float _angle = 360f;
    private float _newCubesSpacing;
    private float radius;
    private Vector3 circularPosition;
    private Vector3 newCubePosition;
    private Vector3 explosionCenter;
    private Vector3 newCubeScale;
    private Cube newCube;

    public void Split(Cube cube)
    {
        if (cube.IsSplit() == false)
        {
            Destroy(cube.gameObject);

            return;
        }

        explosionCenter = cube.transform.position;
        newCubeScale = cube.transform.localScale / _scaleDivider;
        newCubeSplitChance = cube.SplitChance / _SplitChanceDivider;
        radius = cube.transform.localScale.x / _scaleDivider;

        Destroy(cube.gameObject);

        _cubesAmount = Random.Range(_minAmount, _maxAmount) + 1;
        _newCubesSpacing = _angle * Mathf.Deg2Rad;

        for (int i = 1; i <= _cubesAmount; i++)
        {
            circularPosition = new(
                Mathf.Cos(_newCubesSpacing / _cubesAmount * i) * radius,
                0,
                Mathf.Sin(_newCubesSpacing / _cubesAmount * i) * radius);

            newCubePosition = circularPosition + explosionCenter;

            newCube = Instantiate(_prefab, newCubePosition, Quaternion.identity);

            newCube.SetScale(newCubeScale);
            newCube.SetSplitChance(newCubeSplitChance);
            newCube.AddExplosionForce(_explodeForce, explosionCenter, _explodeRadius);
        }
    }
}