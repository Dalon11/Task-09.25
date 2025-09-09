using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public abstract bool CanShoot { get; }
    public abstract void Shoot(Vector2 direction);
}