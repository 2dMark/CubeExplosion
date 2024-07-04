using UnityEngine;

public class EploderCenter : MonoBehaviour
{
    [SerializeField] private ExploderForceTester[] _cubes;

    private void OnMouseDown()
    {
        foreach (var c in _cubes)
        {
            c.AddExplosionForce(transform.position);
        }
    }
}