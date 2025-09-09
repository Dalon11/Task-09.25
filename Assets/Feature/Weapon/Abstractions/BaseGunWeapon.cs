using UnityEngine;

public abstract class BaseGunWeapon : AbstractWeapon
{
    protected static Transform _selectBulletParent;

    [SerializeField] protected AbstractProjectile _projectilePrefab;
    [SerializeField] protected Transform _firePoint;
    [SerializeField] protected WeaponModel _model = new WeaponModel(0.4f, 15f, 1f, 10);

    protected float _nextFireTime = 0f;
    protected ObjectPool<AbstractProjectile> _pool;

    private readonly string _nameBulletsParent = "Bullets";

    protected virtual void Awake() => CreatePool();

    public override bool CanShoot => Time.time >= _nextFireTime;

    protected virtual AbstractProjectile CreateProjectile()
    {
        AbstractProjectile bullet = _pool.GetFromPool();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = _firePoint.position;
        return bullet;
    }

    protected virtual void Reloading() => _nextFireTime = Time.time + _model.FireRate;

    private void CreatePool()
    {
        if (_selectBulletParent == null)
            _selectBulletParent = new GameObject(_nameBulletsParent).transform;

        _pool = new ObjectPool<AbstractProjectile>(_projectilePrefab, _selectBulletParent);
    }
}
