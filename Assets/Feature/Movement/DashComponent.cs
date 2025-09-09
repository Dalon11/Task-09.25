using System;
using UnityEngine;

public class DashComponent : AbstractMovement
{
    [Serializable]
    private class DashModel
    {
        [Min(1.0f)] public float Speed = 5f;
        [Min(2.0f)] public float DashSpeed = 8f;
        [Min(0.1f)] public float DashCooldown = 3f;
        [Min(0.1f)] public float DashDuration = 0.5f;
        [Min(0.1f)] public float MinDashUseDistance = 3.0f;
        [Min(0.1f)] public float MaxDashUseDistance = 8.0f;

        [HideInInspector] public float LastDashTime;
        [HideInInspector] public bool IsDashing;
        [HideInInspector] public float DashEndTime;
    }

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private DashModel _model;
    private bool CanDash => Time.time - _model.LastDashTime >= _model.DashCooldown;


    private void Update()
    {
        if (_model.IsDashing && Time.time >= _model.DashEndTime)
            StopDash();
    }

    public override void MoveTo(Vector2 target)
    {
        TryUseDash(target);
        float currentSpeed = _model.IsDashing ? _model.DashSpeed : _model.Speed;
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        _rigidbody2D.velocity = direction * currentSpeed;
    }

    private void TryUseDash(Vector2 target)
    {
        if (!_model.IsDashing && CanDash)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target);
            if (distanceToTarget <= _model.MaxDashUseDistance &&
                distanceToTarget >= _model.MinDashUseDistance)
                StartDash();
        }
    }

    private void StartDash()
    {
        _model.IsDashing = true;
        _model.LastDashTime = Time.time;
        _model.DashEndTime = Time.time + _model.DashDuration;
    }

    private void StopDash() => _model.IsDashing = false;
}