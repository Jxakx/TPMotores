using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 Joaquin Lopez
public class MoveController
{
    private Transform _transform;
    private float _speed;
    private float _jumpForce;
    private Rigidbody _rB;

    // Constructor
    public MoveController(Transform transform, float speed, float jumpForce, Rigidbody rB)
    {
        _transform = transform;
        _speed = speed;
        _jumpForce = jumpForce;
        _rB = rB;
    }

    public void Move(Vector3 dir)
    {
        Vector3 moveDirection = _transform.right * dir.x + _transform.forward * dir.z;
        _rB.velocity = new Vector3(moveDirection.x * _speed, _rB.velocity.y, moveDirection.z * _speed);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rB.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
