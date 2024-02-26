using UnityEngine;

public class HealthBonus : Bonus
{
    [SerializeField] private int _health = 50;
    private CharacterHealth _healingCharacterHelth;
    protected override bool CheckTriggeredObject(Collider other)
    {
        _healingCharacterHelth = other.GetComponentInParent<PlayerHealth>();
        return _healingCharacterHelth != null;
    }
    protected override void ApplyBonus()
    {
        _healingCharacterHelth.AddHealthPoints(_health);
    }
}
