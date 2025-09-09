using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemComponent : AbstractWeaponSystem
{
    [SerializeField] private List<AbstractWeapon> _weaponPrefabs = new List<AbstractWeapon>();
    [SerializeField] private Transform _weaponMount;

    private List<AbstractWeapon> _currentWeapons = new List<AbstractWeapon>();
    private AbstractWeapon _currentWeapon;
    private int _currentWeaponIndex;

    private void Start() => InitializeWeapons();

    public override void SwitchNextWeapon() => SwitchWeapon(_currentWeaponIndex + 1);

    public override void SwitchWeapon(int index)
    {
        if (index < 0 || index >= _currentWeapons.Count)
            index = 0;

        WeaponSetActive(false);
        _currentWeaponIndex = index;
        _currentWeapon = _currentWeapons[_currentWeaponIndex];
        WeaponSetActive(true);
    }

    public override void Shoot(Vector2 direction)
    {
        if (_currentWeapon?.CanShoot == false)
            return;

        _currentWeapon.Shoot(direction);
    }

    private void InitializeWeapons()
    {
        foreach (AbstractWeapon weaponPrefab in _weaponPrefabs)
        {
            GameObject weaponInstance = Instantiate(weaponPrefab.gameObject, _weaponMount);
            AbstractWeapon weapon = weaponInstance.GetComponent<AbstractWeapon>();
            weapon.gameObject.SetActive(false);
            _currentWeapons.Add(weapon);
        }

        SwitchWeapon(0);
    }

    private void WeaponSetActive(bool isActive) => _currentWeapon?.gameObject.SetActive(isActive);
}