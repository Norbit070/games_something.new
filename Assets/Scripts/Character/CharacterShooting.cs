using System;
using UnityEngine;

public abstract class CharacterShooting : CharacterPart
{
    public const float DefaultDamageMutiplier = 1;
    private float _damageMultiplier = DefaultDamageMutiplier;
    private float _damageMultiplierDuration;
    private float _damageMultiplierTimer;
    public Action<float> OnSetDamageMutiplier;
    public Action<float, float> OnChangeDamageTimer;
    private const string WeaponIdKey = "WeaponId";
    [SerializeField, Range(0, 2)] private int _weaponId;
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
        SetCurrentWeapon(_weaponId);
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
    private void SetCurrentWeapon(int id)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].SetActive(i == id);
        }
        _currentWeapon = _weapons[id];
        _animator.SetInteger(WeaponIdKey, id);
    }
}