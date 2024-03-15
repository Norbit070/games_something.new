
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private CharacterPart[] _parts;
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _parts = GetComponents<CharacterPart>();
        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i] is not null)
            {
                _parts[i].Init();
            }
        }

        InitDeath();
        InitWeaponSelection();
    }

    private void InitDeath()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i] is CharacterHealth health)
            {
                health.OnDie += Stop;
            }
        }
    }

    private void Stop()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            _parts[i].Stop();
        }
    }
    private void InitWeaponSelection()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i] is CharacterWeaponSelector weaponSelector)
            {
                weaponSelector.OnWeaponSelected += SelectWeapon;
                weaponSelector.RefreshSelectedWeapon();
            }
        }
    }
    private void SelectWeapon(WeaponIdentity id)
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i] is IWeaponDependent weaponDependent)
            {
                weaponDependent.SetWeapon(id);
            }
        }
    }

}
