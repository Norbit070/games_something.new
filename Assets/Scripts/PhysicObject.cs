using UnityEngine;


public class PhysicObject : MonoBehaviour, IPhysicHittable
{
    private Rigidbody _rigidbody;

    public void Hit(Vector3 force, Vector3 position)
    {
        CheckRigidbody();
        _rigidbody.AddForceAtPosition(force, position, ForceMode.Impulse);
    }
    private void CheckRigidbody()
    {
        if (!_rigidbody)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}