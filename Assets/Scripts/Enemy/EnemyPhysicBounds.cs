using UnityEngine;

public class EnemyPhysicBounds : CharacterPhysicBounds
{
    private Collider _bodyCollider;

    protected override void OnInit()
    {
        _bodyCollider = GetComponentInChildren<Collider>();
    }
    protected override void OnStop()
    {
        _bodyCollider.enabled = false;
    }
}