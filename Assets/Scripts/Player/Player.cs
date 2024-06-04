using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    MoveController _moveController;
    private Vector3 _direction;
    void Start()
    {
        _moveController = new MoveController(transform, _speed);
    }

    
    void Update()
    {
        
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
            
        }
    }
}
