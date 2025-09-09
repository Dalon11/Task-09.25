using UnityEngine;

public class ShotgunWeapon : BaseGunWeapon
{
    [Min(1)] [SerializeField] private int _bulletsPerShot = 5;
    [SerializeField] private float _spreadAngle = 30f;

    public override void Shoot(Vector2 direction)
    {
        if (!CanShoot) return;

        Vector2[] bulletDirections = CalculateDirections(direction);
        for (int i = 0; i < bulletDirections.Length; i++)
            CreateProjectile().Initialize(bulletDirections[i], _model.Damage, _model.BulletSpeed, _model.BulletLifeTime);

        Reloading();
    }

    private Vector2[] CalculateDirections(Vector2 direction)
    {
        float baseAngleRadians = Mathf.Atan2(direction.y, direction.x);
        float spreadAngleRadians = _spreadAngle * Mathf.Deg2Rad;
        float stepSizeRadians = spreadAngleRadians / (_bulletsPerShot - 1);
        float startAngleRadians = baseAngleRadians - spreadAngleRadians / 2f;
        Vector2[] directions = new Vector2[_bulletsPerShot];
        for (int i = 0; i < _bulletsPerShot; i++)
        {
            float currentAngleRadians = startAngleRadians + stepSizeRadians * i;
            directions[i] = new Vector2(Mathf.Cos(currentAngleRadians), Mathf.Sin(currentAngleRadians));
        }

        return directions;
    }
}
