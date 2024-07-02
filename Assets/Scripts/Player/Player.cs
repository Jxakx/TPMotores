using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 Joaquin Lopez
public class Player : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Rigidbody _rB;
    private MoveController _moveController;
    [SerializeField] public bool isGrounded;

    private void Awake()
    {
        _rB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _moveController = new MoveController(transform, _speed, _jumpForce, _rB);
    }

    void Update()
    {
        Walk();
        Jump();
    }

    private void Walk()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _moveController.Move(direction);
    }

    private void Jump()
    {
        _moveController.Jump(isGrounded);
    }

    public void ReciveLife(int value)
    {
        life += value;
        if (life > 10) life = 10;
    }

    public void ReciveDamage(int value)
    {
        life -= value;
        if (life <= 0)
        {
            life = 0;
            Dead();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
    }
    private void Dead()
    {
        Destroy(GetComponent<Player>(), 1);
    }
}
