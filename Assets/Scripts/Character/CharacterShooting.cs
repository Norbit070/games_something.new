using System;
using UnityEngine;

public abstract class CharacterShooting : CharacterPart, IWeaponDependent
{
    public const float DefaultDamageMutiplier = 1;
    private float _damageMultiplier = DefaultDamageMutiplier;
    private float _damageMultiplierDuration;
    private float _damageMultiplierTimer;
    public Action<float> OnSetDamageMutiplier;
    public Action<float, float> OnChangeDamageTimer;
    private const string WeaponIdKey = "WeaponId";
    private WeaponIdentity _weaponId;
    private Animator _animator;
    private Weapon[] _weapons;
    private Weapon _currentWeapon;
    
    public float DamageMultiplier
    {
        get { return _damageMultiplier; }
    }
    public void SetDamageMultiplier(float multiplier, float duration)
    {
        _damageMultiplier = multiplier;
        _damageMultiplierDuration = duration;
        _damageMultiplierTimer = 0;
        OnSetDamageMutiplier?.Invoke(_damageMultiplier);
        OnChangeDamageTimer?.Invoke(_damageMultiplierTimer, _damageMultiplierDuration);
    }
    protected override void OnInit()
    {
        _animator = GetComponentInChildren<Animator>();
        _weapons = GetComponentsInChildren<Weapon>(true);
        SetDefaultDamageMultiplier();
    }
    protected void DamageBonusing()
    {
        if (_damageMultiplierDuration <= 0)
        {
            return;
        }
        _damageMultiplierTimer += Time.deltaTime;
        OnChangeDamageTimer?.Invoke(_damageMultiplierTimer, _damageMultiplierDuration);
        if (_damageMultiplierTimer >= _damageMultiplierDuration)
        {
            SetDefaultDamageMultiplier();
        }
    }

    protected void SpawnBullet(Bullet prefab, Transform spawnPoint)
    {
        Bullet bullet = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        InitBullet(bullet);
    }
    private void SetDefaultDamageMultiplier()
    {
        SetDamageMultiplier(DefaultDamageMutiplier, 0);
    }

    private void InitBullet(Bullet bullet)
    {
        bullet.SetDamage((int) (_currentWeapon.Damage * _damageMultiplier));
    }
    private void SetCurrentWeapon(WeaponIdentity identity)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            Weapon weapon = _weapons[i];
            bool isTargetId = weapon.Id == identity;
            weapon.SetActive(isTargetId);
            if (isTargetId)
            {
                _currentWeapon = weapon;
            }
        }
        int id = WeaponIdentifier.GetAnimationIdByWeaponIdentify(identity);

        _animator.SetInteger(WeaponIdKey, id);
    }
    public void SetWeapon(WeaponIdentity id)
    {
        _weaponId = id;
        SetCurrentWeapon(_weaponId);
    }
}