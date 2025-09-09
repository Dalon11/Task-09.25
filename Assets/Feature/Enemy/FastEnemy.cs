public class FastEnemy : EnemyBase
{
    private void FixedUpdate() => Movement();

    protected override void Movement() => MoveTowardsTarget();

    protected void MoveTowardsTarget()
    {
        if (_target == null || _health?.IsAlive == false)
            return;
        
        _movement?.MoveTo(_target.position);
    }
}