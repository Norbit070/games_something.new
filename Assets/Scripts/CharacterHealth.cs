using System;
using UnityEngine;

public abstract class CharacterHealth : CharacterPart
{
    private const string DeathKey = "Death";
    private int _startHealthPoints = 100;
    private Animator _animator;
    private int _healthPoints;
    private bool _isDead;
    public Action OnDie;
    public Action<CharacterHealth> OnDieWithObject;
    public Action OnAddHealthPoints;
    
    public void AddHealthPoints(int value)
    {
        if (_isDead)
        {
            return;
        }
        _healthPoints += value;
        OnAddHealthPoints?.Invoke();

        if (_healthPoints <= 0)
        {
            Die();
        }
    }
    protected override void OnInit()
    {
        _animator = GetComponentInChildren<Animator>();
        _healthPoints = _startHealthPoints;
        _isDead = false;
    }
    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger(DeathKey);
        OnDie?.Invoke();
        OnDieWithObject?.Invoke(this);
    }
    public int GetStartHealthPoints()
    {
        return _startHealthPoints;
    }

    public int GetHealthPoints()
    {
        return _healthPoints;
    }
}
