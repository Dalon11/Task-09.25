using UnityEngine;

public class BasicEnemy : EnemyBase
{
    private void FixedUpdate() => Movement();

    protected override void Movement() => MoveTowardsTarget();

    private void MoveTowardsTarget()
    {
        if (_target == null || _health?.IsAlive == false) 
            return;

        Vector2 direction = (_target.position - transform.position).normalized;
        _movement?.MoveTo(direction);
    }
}