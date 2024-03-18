using System;
using UnityEngine;

public abstract class CharacterWeaponSelector : CharacterPart
{
    [SerializeField] protected WeaponIdentity _weaponId;
    public Action<WeaponIdentity> OnWeaponSelected;
    public void RefreshSelectedWeapon()
    {
        OnWeaponSelected?.Invoke(_weaponId);
    }

    protected override void OnInit() 
    {
        RefreshSelectedWeapon();
    }

}