using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
    public abstract int Damage { get; }

    public abstract void Attack(ITakeDamage target);
}
