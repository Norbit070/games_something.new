using UnityEngine;

public class DamageBonus : Bonus
{
    [SerializeField] private float _damageMultiplier = 2f;
    [SerializeField] private float _duration = 10f;
    private CharacterShooting _bonusedCharacterShooting;
    protected override bool CheckTriggeredObject(Collider other)
    {
        _bonusedCharacterShooting = other.GetComponentInParent<PlayerShooting>();
        return _bonusedCharacterShooting != null;
    }
    protected override void ApplyBonus()
    {
        _bonusedCharacterShooting.SetDamageMultiplier (_damageMultiplier, _duration);
    }
}