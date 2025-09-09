using System;
using UnityEngine;

public class HealthComponent : AbstractHealth
{
    [SerializeField] private int _maxHealth = 10;

    private int _currentHealth;

    public override int MaxHealth => _maxHealth;
    public override int CurrentHealth => _currentHealth;
    public override bool IsAlive => _currentHealth > 0;

    public override event Action<int> onHealthChanged = (_) => { };
    public override event Action onDeath = () => { };

    private void Awake() => _currentHealth = _maxHealth;

    public override void TakeDamage(int damage)
    {
        if (!IsAlive) return;

        _currentHealth = Mathf.Max(0, _currentHealth - damage);
        onHealthChanged.Invoke(_currentHealth);
        if (_currentHealth <= 0)
            onDeath.Invoke();
    }

    public override void RestoreHealth() => _currentHealth = _maxHealth;
}