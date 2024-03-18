using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCurrentWeaponView : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _bulletsText;
    [SerializeField] private Sprite _rifleSprite;
    [SerializeField] private Sprite _pistolSprite;
    [SerializeField] private Sprite _shotgunSprite;
    private Dictionary<WeaponIdentity, Sprite> _weaponToSpritePairs;
    private CharacterWeaponSelector _playerWeaponSelector;
    private CharacterShooting _playerShooting;
    private Weapon _playerWeapon;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _weaponToSpritePairs = new Dictionary<WeaponIdentity, Sprite>()
        {
            { WeaponIdentity.Rifle, _rifleSprite },
            { WeaponIdentity.Pistol, _pistolSprite },
            { WeaponIdentity.Shotgun, _shotgunSprite }
        };
        _playerWeaponSelector = FindObjectOfType<PlayerWeaponSelector>();
        _playerShooting = FindObjectOfType<PlayerShooting>();
        _playerWeaponSelector.OnWeaponSelected += SetIconByType;
        _playerShooting.OnSetCurrentWeapon += SubscribeForBullets;
    }
    private void SubscribeForBullets(Weapon weapon)
    {
        weapon.OnBulletsInRowChange += SetBulletText;

        if (_playerWeapon)
        {
            _playerWeapon.OnBulletsInRowChange -= SetBulletText;
        }
        _playerWeapon = weapon;
    }
    private void SetIconByType(WeaponIdentity weaponId)
    {
        _iconImage.sprite = _weaponToSpritePairs[weaponId];
    }

    private void SetBulletText(int currentBulletsInRow, int bulletsInRow)
    {
        _bulletsText.text = $"{currentBulletsInRow}/{bulletsInRow}";
    }

    private void OnDestroy()
    {
        if (_playerWeaponSelector)
        {
            _playerWeaponSelector.OnWeaponSelected -= SetIconByType;
        }
        if (_playerShooting)
        {
            _playerShooting.OnSetCurrentWeapon -= SubscribeForBullets;
        }
        if (_playerWeapon)
        {
            _playerWeapon.OnBulletsInRowChange -= SetBulletText;
        }
    }
}
