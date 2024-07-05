using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private float _maxPercent = 100;
    private float _splitChance = 100;

    public float SplitChance => _splitChance;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        SetRandomColor();
    }

    public bool IsSplit()
    {
        float randomNumber = Random.Range(0, _maxPercent);

        if (_splitChance >= randomNumber)
            return true;

        return false;
    }

    public void SetScale(Vector3 scale) => transform.localScale = scale;

    public void SetSplitChance(float splitChance) => _splitChance = splitChance;

    public void AddExplosionForce
        (float explosionForce, Vector3 explosionPosition, float explosionRadius)
        => _rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);

    private void SetRandomColor() => _renderer.material.color = Random.ColorHSV();
}