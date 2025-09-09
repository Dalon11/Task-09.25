using System;
using UnityEngine;

[Serializable]
public class WeaponModel
{
    [Range(0.1f, 1f)] [SerializeField] private float _fireRate;
    [Range(0.1f,1.0f)] [SerializeField] private float _bulletLifeTime;
    [Min(1.0f)] [SerializeField] private float _bulletSpeed;
    [Min(0)] [SerializeField] private int _damage;

    public WeaponModel(float fireRate, float bulletSpeed, float bulletLifeTime, int damage)
    {
        _fireRate = fireRate;
        _bulletSpeed = bulletSpeed;
        _bulletLifeTime = bulletLifeTime;
        _damage = damage;
    }

    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }
    public float BulletLifeTime { get => _bulletLifeTime; set => _bulletLifeTime = value; }
    public int Damage { get => _damage; set => _damage = value; }
}
