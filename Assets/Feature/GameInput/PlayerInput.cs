using System;
using UnityEngine;

public class PlayerInput : AbstractInput
{
    [SerializeField] private KeyCode _shootActionKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _switchWeaponActionKey = KeyCode.Mouse1;

    private float _horizontal;
    private float _vertical;

    private readonly string _horizontalAxisName = "Horizontal";
    private readonly string _verticalAxisName = "Vertical";
    public override float Horizontal => _horizontal;
    public override float Vertical => _vertical;

    public override event Action onShoot = () => { };
    public override event Action onSwitchWeapon = () => { };

    private void Update() => HandleInput();
    private void HandleInput()
    {
        _horizontal = Input.GetAxisRaw(_horizontalAxisName);
        _vertical = Input.GetAxisRaw(_verticalAxisName);

        if (Input.GetKey(_shootActionKey))
            onShoot.Invoke();
        if (Input.GetKeyDown(_switchWeaponActionKey))
            onSwitchWeapon.Invoke();
    }
}
