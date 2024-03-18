using System.Collections.Generic;

public static class WeaponIdentifier
{
    private static Dictionary<WeaponIdentity, int> _weaponIdentityToAnimationIdPairs =
        new Dictionary<WeaponIdentity, int>
        {
            { WeaponIdentity.Rifle, 0 },
            { WeaponIdentity.Pistol, 1 },
            { WeaponIdentity.Shotgun, 2 }
        };
    
    public static int GetAnimationIdByWeaponIdentify(WeaponIdentity identity)
    {
        return _weaponIdentityToAnimationIdPairs[identity];
    }
}

public enum WeaponIdentity
{
    Rifle,
    Pistol,
    Shotgun
}