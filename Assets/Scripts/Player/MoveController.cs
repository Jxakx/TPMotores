using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController 
{
    Transform _transform;
    float _speed;
    private float _xAxis;
    private float _zAxis;
    private Rigidbody _rB;
    public MoveController (Transform t, float speed)
    {
        _transform = t;
        _speed = speed;
    }
    public void Move(float vertical, float horizontal)
    {
        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");
        _direction = (this._transform.right * _xAxis + this._transform.forward * _zAxis) * _speed;
        _rB.velocity = new Vector3(_direction.x, _rB.velocity.y, _direction.z);
    }

    private void Jump()
    {
        _rB.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
