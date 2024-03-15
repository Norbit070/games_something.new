using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract WeaponIdentity Id { get; }
    [SerializeField] private int _damage = 10;
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
    }
}