using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private float _startScale = 10f;
    [SerializeField] private int _minAmount = 2;
    [SerializeField] private int _maxAmount = 6;
    [SerializeField] private float _explodeForce = 100f;
    [SerializeField] private float _explodeRadius = 100f;
    [SerializeField] private Transform _transform;

    private int _generation = 0;
    private int _maxPercent = 100;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private int SplitChance => Mathf.RoundToInt(_maxPercent / Mathf.Pow(2, _generation));

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        SetRandomColor();
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Z) && _transform != null)
            AddExplosionForce(_explodeForce, _transform.position, _explodeRadius);
    }

    public bool IsSplit()
    {
        int randomNumber = Random.Range(0, _maxPercent);

        if (SplitChance >= randomNumber)
            return true;

        return false;
    }

    public void SetGeneration(int generation) => _generation = generation;

    private Vector3 explosionPositionGizmo;
    private float explosionRadiusGizmo;

    public void AddExplosionForce
        (float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        explosionRadiusGizmo = explosionRadius;
        explosionPositionGizmo = explosionPosition;

        _rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        Debug.Log($"\nfrom {explosionPosition}\nto {transform.position}");
    }

    private void OnDrawGizmos()
    {
        if (explosionRadiusGizmo != 0)
        {
            Gizmos.DrawWireSphere(explosionPositionGizmo, explosionRadiusGizmo);
        }
    }

    private void SetRandomColor() => _renderer.material.color = Random.ColorHSV();
}