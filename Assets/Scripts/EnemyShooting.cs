using UnityEngine;

public class EnemyShooting : CharacterShooting
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletDelay = 0.05f;
    [SerializeField] private float _shootingRange = 10f;
    [SerializeField] private int _bulletsInRow = 7;
    [SerializeField] private float _reloadingDuration = 4f;
    private Transform _bulletSpawnPoint;

    private Transform _targetTransform;
    private float _bulletTimer;
    private int _currentBulletsInRow;

    protected override void OnInit()
    {
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;
        _targetTransform = FindAnyObjectByType<Player>().transform;
        Reload();
    }

    private void Reload()
    {
        _bulletTimer = 0;
        _currentBulletsInRow = _bulletsInRow;
    }
    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        _bulletTimer += Time.deltaTime;
        Shooting();
        Reloading();
    }

    private void Shooting()
    {
        if (CheckTargetInRange() && CheckHasBulletsInRow() && _bulletTimer >= _bulletDelay)
        {
            Shoot();
        }
    }

    private void Reloading()
    {
        if (!CheckHasBulletsInRow() && _bulletTimer >= _reloadingDuration)
        {
            Reload();
        }
    }
    private bool CheckTargetInRange()
    {
        return (_targetTransform.position - transform.position).magnitude <= _shootingRange;
    }

    private bool CheckHasBulletsInRow()
    {
        return _currentBulletsInRow > 0;
    }
    private void Shoot()
    {
        _bulletTimer = 0;
        SpawnBullet();
        _currentBulletsInRow--;
    }

    private void SpawnBullet()
    {
        Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
    }
}