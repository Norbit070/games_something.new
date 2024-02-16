using UnityEngine;

public class PlayerShooting : CharacterShooting
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletDelay = 0.05f;
    private Transform _bulletSpawnPoint;
    private float _bulletTimer;

    protected override void OnInit()
    {
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;
        _bulletTimer = 0;
    }

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }

        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetMouseButton(0))
        {
            _bulletTimer += Time.deltaTime;

            if (_bulletTimer >= _bulletDelay)
            {
                _bulletTimer = 0;

                SpawnBullet();
            }
        }
    }

    private void SpawnBullet()
    {
        Debug.Log(_bulletSpawnPoint);
        Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
    }
}
 
