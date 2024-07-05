using UnityEngine;

[RequireComponent(typeof(CubeSplitter))]

public class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private CubeSplitter _cubeSplitter;
    private Ray _ray;
    private RaycastHit _raycastHit;

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
    }
}