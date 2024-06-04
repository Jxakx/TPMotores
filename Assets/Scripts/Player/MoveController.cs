using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController 
{
    public Transform transform;
    public float _speed;
    private float _jumpForce;
    private float _xAxis;
    private float _zAxis;
    private Rigidbody _rB;
    public MoveController (Transform t, float speed, float jumpForce)
    {
        this.transform = t;
        _speed = speed;
        _jumpForce = jumpForce;
    }
    public void Move(Vector3 dir)
    {
        transform.position += dir.normalized * _speed * Time.deltaTime;
        //dir = (this.transform.right * _xAxis + this.transform.forward * _zAxis) * _speed * Time.deltaTime;
        //_rB.velocity = new Vector3(dir.x, _rB.velocity.y, dir.z);

    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //_rB.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
            
    }
}
