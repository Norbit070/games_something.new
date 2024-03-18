using UnityEngine;

public class WeaponShotgun : Weapon
{
    [SerializeField] int _bulletsInOneShoot = 10;

    public override WeaponIdentity Id => WeaponIdentity.Shotgun;
    protected override void DoShoot(float damageMultiplier)
    {
        for (int i = 0; i < _bulletsInOneShoot; i++)
        {
            DefaultShoot(damageMultiplier);
        }
    }
}
