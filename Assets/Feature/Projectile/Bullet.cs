using System.Collections;
using UnityEngine;

public class Bullet : AbstractProjectile, IPooling
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private int _damage;
    private Coroutine _lifeCoroutine;
    private WaitForSeconds _waitForLifeTime;

    private void OnEnable() => StartLifeTimer();

    private void Start() => StartLifeTimer();

    public override void Initialize(Vector2 direction, int damage, float speed, float lifetime)
    {
        _damage = damage;
        _waitForLifeTime = new WaitForSeconds(lifetime);
        MoveTo(direction.normalized, speed);
    }

    protected override void Attack(ITakeDamage target) => target.TakeDamage(_damage);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITakeDamage target))
            Attack(target);

        EndLifeBullet();
    }

    private void MoveTo(Vector2 direction, float speed) => _rigidbody2D.velocity = direction * speed;

    private void EndLifeBullet() => gameObject.SetActive(false);
    private IEnumerator LifeTimer()
    {
        yield return _waitForLifeTime;
        EndLifeBullet();
    }
    private void StartLifeTimer()
    {
        if (_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);

        _lifeCoroutine = StartCoroutine(LifeTimer());
    }

}