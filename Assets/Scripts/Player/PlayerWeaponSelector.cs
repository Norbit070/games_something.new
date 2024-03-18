using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelector : CharacterWeaponSelector
{
    Dictionary<KeyCode, WeaponIdentity> _weaponKeyToIdentityPairs = new Dictionary<KeyCode, WeaponIdentity>()
    {
        { KeyCode.Alpha1, WeaponIdentity.Rifle },
        { KeyCode.Alpha2, WeaponIdentity.Pistol },
        { KeyCode.Alpha3, WeaponIdentity.Shotgun }
    };

    private void Update()
    {
        CheckChangeKey();
    }

    private void CheckChangeKey()
    {
        foreach (var pair in _weaponKeyToIdentityPairs)
        {
            if (Input.GetKeyDown(pair.Key))
            {
                _weaponId = pair.Value;
                RefreshSelectedWeapon();
            }
        }
    }
}
