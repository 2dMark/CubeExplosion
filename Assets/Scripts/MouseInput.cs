using UnityEngine;

[RequireComponent(typeof(CubeSplitter))]

public class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CubePool _cubePool;

    private CubeSplitter _cubeSplitter;
    Ray _ray;
    RaycastHit _raycastHit;

    private void Awake()
    {
        _cubeSplitter = GetComponent<CubeSplitter>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _raycastHit))
                if (_raycastHit.collider.TryGetComponent(out Cube cube))
                    _cubeSplitter.Split(cube);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _raycastHit))
                if (_raycastHit.collider.TryGetComponent(out Cube cube))
                    cube.SetGeneration(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _raycastHit))
            {
                Cube cube = Instantiate(_cubePool.Prefab);
                //Cube cube = _cubePool.Get();

                cube.transform.position = new Vector3(
                    _raycastHit.point.x,
                    _raycastHit.point.y + cube.transform.localScale.y,
                    _raycastHit.point.z);
            }
        }
    }
}