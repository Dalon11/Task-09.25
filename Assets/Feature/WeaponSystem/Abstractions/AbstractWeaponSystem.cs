using UnityEngine;

public abstract class AbstractWeaponSystem  : MonoBehaviour
{
    public abstract void Shoot(Vector2 direction);
    public abstract void SwitchNextWeapon();
    public abstract void SwitchWeapon(int index);
}