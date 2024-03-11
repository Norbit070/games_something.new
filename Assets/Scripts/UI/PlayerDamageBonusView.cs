using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerDamageBonusView : MonoBehaviour
{
    [SerializeField] private Image _percentsImage;
    [SerializeField] private TextMeshProUGUI _multiplierText;
    private CharacterShooting _characterShooting;
    private bool _isActive;
    
    private void Start()
    {
        CharacterShooting characterShooting = FindAnyObjectByType<PlayerShooting>();
        Init(characterShooting);
    }

    public void Init(CharacterShooting characterShooting)
    {
        _characterShooting = characterShooting;
        characterShooting.OnSetDamageMutiplier += RefreshText;
        characterShooting.OnChangeDamageTimer += RefreshPercents;
        SetActive(false);
    }
    private void RefreshText(float multiplier)
    {
        RefreshActivityByMultiplier(multiplier);
        _multiplierText.text = $"x{(int)multiplier}";
    }

    private void RefreshActivityByMultiplier(float multiplier)
    {
        if (Mathf.Approximately(multiplier, CharacterShooting.DefaultDamageMutiplier))
        {
            SetActive(false);
        }
        else
        {
            SetActive(true);
        }
    }

    private void RefreshPercents(float timer, float duration)
    {
        if (!_isActive)
        {
            return;
        }
        if (timer >= duration)
        {
            SetActive(false);
        }
        else
        {
            _percentsImage.fillAmount = 1 - timer / duration;
        }
    }
    private void SetActive(bool value)
    {
        gameObject.SetActive(value);
        _isActive = value;
    }

    private void OnDestroy()
    {
        if (_characterShooting)
        {
            _characterShooting.OnSetDamageMutiplier -= RefreshText;
            _characterShooting.OnChangeDamageTimer -= RefreshPercents;
        }
    }

}
