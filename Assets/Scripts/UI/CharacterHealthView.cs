

using UnityEngine;

public abstract class CharacterHealthView : MonoBehaviour
{
    [SerializeField] private Transform _percentsImageTransform;

    private CharacterHealth _characterHealth;
    public void Init(CharacterHealth characterHealth)
    {
        _characterHealth = characterHealth;
        _characterHealth.OnAddHealthPoints += Refresh;
    }
    private void Refresh()
    {
        float percents = (float) _characterHealth.GetHealthPoints() / _characterHealth.GetStartHealthPoints();
        percents = Mathf.Clamp01(percents);
        SetPercents(percents);
    }
    private void SetPercents(float value)
    {
        _percentsImageTransform.localScale = new Vector3(value, 1, 1);
    }
    private void OnDestroy()
    {
        _characterHealth.OnAddHealthPoints -= Refresh;
    }

}
