using UnityEngine;

// Используем анимацию
using UnityEngine.Animations.Rigging;

public abstract class CharacterAiming : CharacterPart, IWeaponDependent
{
    private WeaponAiming[] _weaponAimings;
    private RigBuilder _rigBuilder;
    private WeaponIdentity _weaponId;

    public void SetWeapon(WeaponIdentity id)
    {
        _weaponId = id;
        SetCurrentWeapon(_weaponId);
    }
    protected override void OnInit()
    {
        _weaponAimings = GetComponentsInChildren<WeaponAiming>(true);
        _rigBuilder = GetComponentInChildren<RigBuilder>();
    }
    protected void InitWeaponAimings(Transform aim)
    {
        for (int i = 0; i < _weaponAimings.Length; i++)
        {
            _weaponAimings[i].Init(aim);
        }
        _rigBuilder.Build();
    }
    private void SetCurrentWeapon(WeaponIdentity identity)
    {
        for (int i = 0; i < _weaponAimings.Length; i++)
        {
            WeaponAiming weaponAiming = _weaponAimings[i];
            bool isTargetId = weaponAiming.Id == identity;
            weaponAiming.SetActive(isTargetId);
        }
    }
}