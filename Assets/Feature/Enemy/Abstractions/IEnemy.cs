using UnityEngine;

public interface IEnemy : ITakeDamage
{
    public void Resurrection();

    public void SetTarget(Transform target);
}