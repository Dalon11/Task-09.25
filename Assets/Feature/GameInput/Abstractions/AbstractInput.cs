using System;
using UnityEngine;

public abstract class  AbstractInput: MonoBehaviour
{
    public abstract float Horizontal { get; }
    public abstract float Vertical { get; }

    public abstract event Action onShoot;
    public abstract event Action onSwitchWeapon;
}