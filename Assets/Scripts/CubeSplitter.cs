using System.Collections.Generic;
using UnityEngine;

public class CubeSplitter : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private float _explodeForce = 1000f;
    [SerializeField] private float _explodeRadius = 1000f;
    [SerializeField] private int _minAmount = 2;
    [SerializeField] private int _maxAmount = 6;

    private int _cubesAmount;
    private int _cubeGeneration;
    //private Vector3 _eplosionCenter;
    private float _angle = 360f;
    private float _distance;

    public void Split(Cube cube)
    {
        if (cube.IsSplit() == false)
        {
            _cubePool.Put(cube);

            return;
        }

        Vector3 explosionCenter = cube.transform.position;
        float radius = cube.transform.localScale.x;

        Debug.Log($"explosionCenter-{explosionCenter}");

        //_cubePool.Put(cube);

        _cubesAmount = Random.Range(_minAmount, _maxAmount);
        _distance = _angle * Mathf.Deg2Rad;

        //List<Cube> newCubes = new();
        Cube newCube;

        for (int i = 1; i <= _cubesAmount; i++)
        {
            newCube = Instantiate(_cubePool.Prefab);
            //newCube = _cubePool.Get();

            Vector3 cubePosition = new(
                Mathf.Cos(_distance / _cubesAmount * i) * radius,
                0,
                Mathf.Sin(_distance / _cubesAmount * i) * radius);

            Debug.Log($"cubePosCircle-{cubePosition}");

            Vector3 newPosition = cubePosition + explosionCenter;

            Debug.Log($"newPos-{newPosition}");

            newCube.transform.position = newPosition;

            newCube.AddExplosionForce(_explodeForce, explosionCenter, _explodeRadius);

            //newCubes.Add(newCube);

            //Debug.Log($"{i} - zeroCubePos - {_cubeZeroPosition}");
            //Debug.Log($"{i} - cubePos - {cubePosition}");
            //Debug.Log($"{i} - newCubes - {newCubes[i].transform.position}");
        }

        Destroy(cube.gameObject);

        //foreach (Cube cube1 in newCubes)
        //    cube1.AddExplosionForce(_explodeForce, eplosionCenter, _explodeRadius);
    }
}