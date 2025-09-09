using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour, IPooling
{
    public abstract void Initialize(Vector2 direction, int damage, float speed, float lifetime);

    protected abstract void Attack(ITakeDamage target);
}