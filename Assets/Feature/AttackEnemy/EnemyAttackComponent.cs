using UnityEngine;

public class EnemyAttackComponent : AbstractAttack
{
    [SerializeField] private int _damage = 10;

    public override int Damage => _damage;

    public override void Attack(ITakeDamage target) => target.TakeDamage(_damage);
}
