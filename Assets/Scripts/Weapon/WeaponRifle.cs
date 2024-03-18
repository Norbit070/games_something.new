public class WeaponRifle : Weapon
{ 
    public override WeaponIdentity Id => WeaponIdentity.Rifle;
    protected override void DoShoot(float damageMultiplier)
    {
        DefaultShoot(damageMultiplier);
    }
}