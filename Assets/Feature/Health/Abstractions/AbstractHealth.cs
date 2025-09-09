using System;
using UnityEngine;

public abstract class AbstractHealth : MonoBehaviour, ITakeDamage
{
    public abstract int CurrentHealth { get; }
    public abstract int MaxHealth { get; }
    public abstract bool IsAlive { get; }

    public abstract event Action<int> onHealthChanged;
    public abstract event Action onDeath;

    public abstract void RestoreHealth();
    public abstract void TakeDamage(int damage);
}
