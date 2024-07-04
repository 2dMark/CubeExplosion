using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ExploderForceTester : MonoBehaviour
{
    [SerializeField] private Transform _explosionPosition;
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _explosionRadius = 10f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        _rigidbody.AddExplosionForce(_explosionForce, _explosionPosition.transform.position, _explosionRadius);
    }

    public void AddExplosionForce(Vector3 explosionPosition)
    {
        _rigidbody.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
    }
}