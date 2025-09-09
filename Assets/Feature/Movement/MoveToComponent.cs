using UnityEngine;

public class MoveToComponent : AbstractMovement
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public override void MoveTo(Vector2 direction) => _rigidbody2D.velocity = direction * _speed;
}
