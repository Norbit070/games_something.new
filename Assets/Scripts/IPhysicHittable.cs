using UnityEngine;

public interface IPhysicHittable
{
    void Hit(Vector3 force, Vector3 position);
}
