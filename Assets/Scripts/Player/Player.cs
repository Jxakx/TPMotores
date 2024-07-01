using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 Joaquin Lopez
public class Player : MonoBehaviour
{
    [SerializeField] private float life = 10f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    private Rigidbody _rB;
    private MoveController _moveController;

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
        _moveController.Jump();
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

    private void Dead()
    {
        // Lógica para la muerte del personaje
    }
}
