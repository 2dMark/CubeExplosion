using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Collider _collider;
    private float _maxPercent = 100;
    private float _splitChance = 100;

    public float SplitChance => _splitChance;

    public Collider Collider => _collider;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        SetRandomColor();
    }

    public bool IsSplit() => _splitChance >= Random.Range(0, _maxPercent);

    public void SetScale(Vector3 scale) => transform.localScale = scale;

    public void SetSplitChance(float splitChance) => _splitChance = splitChance;

    private void SetRandomColor() => _renderer.material.color = Random.ColorHSV();
}