using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour, ITakeDamage
{
    [SerializeField] private AbstractInput _input;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _components;

    private AbstractWeaponSystem _weapon;
    private AbstractHealth _health;
    private AbstractMovement _movement;

    private void Awake() => Construct();

    private void FixedUpdate() => Movement();

    private void OnDestroy() => Unsubscribe();

    public void TakeDamage(int damage) => _health?.TakeDamage(damage);

    private void Construct()
    {
        _components.TryGetAndUseComponent(out _weapon);
        _components.TryGetAndUseComponent(out _health);
        _components.TryGetAndUseComponent(out _movement);
        Subscribe();
    }

    private void Movement()
    {
        if (_health?.IsAlive == false)
            return;

        Vector2 direction = new Vector2(_input.Horizontal, _input.Vertical);
        _movement?.MoveTo(direction);
    }

    private void Shooting()
    {
        if (_health?.IsAlive == false)
            return;

        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector2 direction = (mousePosition - transform.position).normalized;
        _weapon?.Shoot(direction);
    }

    private void SwitchWeapon() => _weapon?.SwitchNextWeapon();

    private void OnDeath() => _movement?.MoveTo(Vector2.zero);

    private void Subscribe()
    {
        _input.onShoot += Shooting;
        _input.onSwitchWeapon += SwitchWeapon;
        if (_health)
            _health.onDeath += OnDeath;
    }

    private void Unsubscribe()
    {
        _input.onShoot -= Shooting;
        _input.onSwitchWeapon -= SwitchWeapon;
        if (_health)
            _health.onDeath -= OnDeath;
    }
}
