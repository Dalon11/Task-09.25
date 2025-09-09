using UnityEngine;

[SelectionBase]
public abstract class EnemyBase : MonoBehaviour, IEnemy, IPooling
{
    [SerializeField] protected GameObject _components;

    protected AbstractHealth _health;
    protected AbstractMovement _movement;
    protected AbstractAttack _attack;
    protected Transform _target;

    protected virtual void Awake() => Construct();

    protected virtual void OnDestroy() => Unsubscribe();

    public void Resurrection() => _health?.RestoreHealth();

    public virtual void TakeDamage(int damage) => _health.TakeDamage(damage);

    public virtual void SetTarget(Transform target) => _target = target;

    protected abstract void Movement();

    protected virtual void Construct()
    {
        _components.TryGetAndUseComponent(out _health);
        _components.TryGetAndUseComponent(out _movement);
        _components.TryGetAndUseComponent(out _attack);
        Subscribe();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITakeDamage target))
        {
            _attack?.Attack(target);
            OnDeath();
        }
    }

    protected virtual void OnDeath() => gameObject.SetActive(false);

    protected virtual void Subscribe()
    {
        if (_health)
            _health.onDeath += OnDeath;
    }

    protected virtual void Unsubscribe()
    {
        if (_health)
            _health.onDeath -= OnDeath;
    }
}
