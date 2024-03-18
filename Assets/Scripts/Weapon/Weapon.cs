using UnityEngine;
using System;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletDelay = 0.05f;
    [SerializeField] private int _bulletsInRow = 7;
    [SerializeField] private float _reloadingDuration = 4f;
    [SerializeField] private float _spreadAngle = 5f;
    private Transform _bulletSpawnPoint;
    private int _currentBulletsInRow;
    private float _bulletTimer;
    private float _reloadingTimer;
    private bool _isShootDelayEnd;
    private bool _isReloading;
    public Action<int, int> OnBulletsInRowChange;
    
    public abstract WeaponIdentity Id { get; }
    [SerializeField] private int _damage = 10;
    protected abstract void DoShoot(float damageMultiplier);

    protected void DefaultShoot(float damageMultiplier)
    {
        SpawnBullet(_bulletPrefab, _bulletSpawnPoint, damageMultiplier);
    }
    
    public void Init()
    {
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;
        FillBulletsToRow();
    }
    public int Damage
    {
        get
        {
            return _damage;
        }
    } 
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        OnBulletsInRowChange?.Invoke(_currentBulletsInRow, _bulletsInRow);
    }
    
    public void Shoot(float damageMultiplier)
    {
        if (!_isShootDelayEnd || !CheckHasBulletsInRow())
        {
            return;
        }
        _bulletTimer = 0;
        DoShoot(damageMultiplier);
        _currentBulletsInRow--;
        OnBulletsInRowChange?.Invoke(_currentBulletsInRow, _bulletsInRow);
    }
    public void Reload()
    {
        if (_isReloading)
        {
            _reloadingTimer += Time.deltaTime;
            return;
        }
        _isReloading = true;
    }
    
    
    private void Reloading()
    {
        if (_isReloading)
        {
            _reloadingTimer += Time.deltaTime;
            if (_reloadingTimer >= _reloadingDuration)
            {
                FillBulletsToRow();
            }
        }
    }
    
    
    public bool CheckHasBulletsInRow()
    {
        return _currentBulletsInRow > 0;
    }
    private void SpawnBullet(Bullet prefab, Transform spawnPoint, float damageMultiplier)
    {
        Bullet bullet = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        Vector3 bulletEulerAngles = bullet.transform.eulerAngles;
        bulletEulerAngles.x += Random.Range(-_spreadAngle, _spreadAngle);
        bulletEulerAngles.y += Random.Range(-_spreadAngle, _spreadAngle);
        bullet.transform.eulerAngles = bulletEulerAngles;
        InitBullet(bullet, damageMultiplier);
    }

    private void InitBullet(Bullet bullet, float damageMultiplier)
    {
        bullet.SetDamage((int)(_damage * damageMultiplier));
    }
    private void Update()
    {
        ShootDelaying();
        Reloading();
    }

    private void ShootDelaying()
    {
        _bulletTimer += Time.deltaTime;
        _isShootDelayEnd = _bulletTimer >= _bulletDelay;
    }

    
    private void FillBulletsToRow()
    {
        _isReloading = false;
        _reloadingTimer = 0;
        _currentBulletsInRow = _bulletsInRow;
        OnBulletsInRowChange?.Invoke(_currentBulletsInRow, _bulletsInRow);
    }
    
}