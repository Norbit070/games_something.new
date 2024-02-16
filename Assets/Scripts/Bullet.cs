using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _lifeTime = 2f;

    private void Update()
    {
        ReduceLifeTime();
        CheckHit();
        Move();
    }

    private void ReduceLifeTime()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            DestroyBullet();
        }
    }

    private void CheckHit()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _speed * Time.deltaTime))
        {
            Hit(hit);
        }
    }

    private void Move()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void Hit(RaycastHit hit)
    {
        CheckCharacterHit(hit);
        Instantiate(_hitPrefab, hit.point, Quaternion.LookRotation(-transform.up, -transform.forward));
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
    private void CheckCharacterHit(RaycastHit hit)
    {
        CharacterHealth hittedHealth = hit.collider.GetComponentInChildren<CharacterHealth>();
        if (hittedHealth)
        {
            int damage = 10;
            hittedHealth.AddHealthPoints(-damage);
        }
    }
}