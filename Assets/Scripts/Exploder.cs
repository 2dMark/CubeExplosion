using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _basicExplosionForce = 500f;
    [SerializeField] private float _basicExplosionRadius = 50f;

    private Collider[] _targets;
    private float _explosionForce;
    private float _explosionRadius;

    public void ExplodeTargets
        (float explosionForceDivisor, Vector3 explosionPosition, float explosionRadiusDivisor)
    {
        _targets = Physics.OverlapSphere(explosionPosition, _explosionRadius);

        ExplodeTargets(explosionForceDivisor, explosionPosition,
            explosionRadiusDivisor, _targets);
    }

    public void ExplodeTargets(float explosionForceDivisor, Vector3 explosionPosition,
        float explosionRadiusDivisor, Collider[] targets)
    {
        _explosionForce = _basicExplosionForce / explosionForceDivisor;
        _explosionRadius = _basicExplosionRadius / explosionRadiusDivisor;

        Explode(_explosionForce, explosionPosition, _explosionRadius, targets);
    }

    private void Explode
        (float explosionForce, Vector3 explosionPosition,
        float explosionRadius, Collider[] targets)
    {
        foreach (Collider target in targets)
            if (target.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
    }
}