using UnityEngine;

public class PistolWeapon : BaseGunWeapon
{
    public override void Shoot(Vector2 direction)
    {
        if (!CanShoot) return;

        CreateProjectile().Initialize(direction, _model.Damage, _model.BulletSpeed, _model.BulletLifeTime);
        Reloading();
    }
}
