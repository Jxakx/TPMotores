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

    public int speedAssaultGoat;

    private bool isCharging = false;
    private float chargingSpeed = 10.0f; // Velocidad máxima que se puede cargar
    private float chargeTime = 2.0f; // Tiempo máximo de carga en segundos
    private float currentChargeTime = 0.0f; // Tiempo actual de carga
    private float chargeVelocity = 0.0f; // Velocidad actual de la carga
    private float shootForceMultiplier = 50.0f; // Multiplicador de la fuerza de disparo


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
        if (!isCharging)
        {
            Walk();
        }
        Jump();
        assaultGoat();
    }

    private void Walk()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _moveController.Move(direction * _speed);
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

    public void assaultGoat()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isCharging = true;
            // Detener el movimiento del jugador al cargar la embestida
            _moveController.Move(Vector3.zero);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (currentChargeTime < chargeTime)
            {
                currentChargeTime += Time.deltaTime;

            }
            else
            {
                currentChargeTime = chargeTime; // No permitir que el tiempo de carga exceda chargeTime
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            if (isCharging)
            {
                // Calcular la fuerza de disparo basada en el tiempo de carga
                float shootForce = currentChargeTime / chargeTime * chargingSpeed * shootForceMultiplier;

                // Aplicar fuerza hacia adelante al Rigidbody para disparar al jugador
                _rB.AddForce(transform.forward * shootForce, ForceMode.Impulse);

                // Reiniciar variables de carga
                isCharging = false;
                currentChargeTime = 0.0f;
            }
        }


    }
}
