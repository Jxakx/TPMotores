using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

//TP2 Joaquin Lopez
public class Player : MonoBehaviour, IDamageable
{
    public enum PlayerState
    {
        Healthy,
        Injured,
        Critical
    }

    [SerializeField] private int life;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Rigidbody _rB;
    private MoveController _moveController;
    [SerializeField] public bool isGrounded;
    private bool isOnTrapPlatform;

    public int speedAssaultGoat;
    public int assaultDamage;
    private bool isAssaulting;
    private bool isCharging = false;
    private float chargingSpeed = 10.0f;
    private float chargeTime = 2.0f;
    private float currentChargeTime = 0.0f;
    private float shootForceMultiplier = 50.0f;

    public GameObject shootBall;
    [SerializeField] Transform shootPoint;
    public ShootPlayer shootPlayer;

    public event EventHandler OnJump;
    public GameplayCanvasManager gamePlayCanvas;
    private GameManager _gameManager;

    private bool isShootingCharging = false;
    private float defaultShootCooldown = 0.2f;
    private float increasedShootCooldown = 0.8f;
    private float currentShootCooldown = 0.0f;

    private PlayerState playerState;

    public delegate void JumpAction();
    public static event JumpAction OnJumpEvent; // Evento para el Delegate cuando salta (Plataformas)

    public delegate void LifeChanged(int newLife);
    public static event LifeChanged OnLifeChanged; // Evento para la actualización ed vida

    public int Life
    {
        get => life;
        private set
        {
            life = Mathf.Clamp(value, 0, 10);
            OnLifeChanged?.Invoke(life);
            UpdatePlayerState();
            if (life == 0) Dead();
        }
    }

    public float Speed
    {
        get => _speed;
        set => _speed = Mathf.Max(0, value);
    }

    public float JumpForce
    {
        get => _jumpForce;
        set => _jumpForce = Mathf.Max(0, value);
    }

    public PlayerState CurrentState => playerState;

    public bool IsGrounded
    {
        get => isGrounded;
        private set => isGrounded = value;
    }



    private void Awake()
    {
        _rB = GetComponent<Rigidbody>();
        _gameManager = FindAnyObjectByType<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager no encontrado");
        }
        UpdatePlayerState();
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

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || isOnTrapPlatform))
        {
            OnJump?.Invoke(this, EventArgs.Empty);
            Jump();
        }
        assaultGoat();
        shootRock();

        if (currentShootCooldown > 0)
        {
            currentShootCooldown -= Time.deltaTime;
        }
    }

    private void Walk()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _moveController.Move(direction * _speed);
    }

    private void Jump()
    {
        _moveController.Jump(isGrounded || isOnTrapPlatform);
        isGrounded = false;
        isOnTrapPlatform = false;

        OnJumpEvent?.Invoke(); // Notifica a las plataformas que el jugador saltó (Delegate)
    }

    public void ReciveLife(int value)
    {
        life += value;
        
        OnLifeChanged?.Invoke(life); // Notifica a la UI (Delegate)
    }

    public void TakeDamage(int value)
    {
        life -= value;
        
        if (OnLifeChanged != null)
        {
            OnLifeChanged.Invoke(life); // Notifica a la UI (Delegate)
        }

        if(life == 0)
        {
            Dead();
        }
    }

    private void UpdatePlayerState()
    {
        if (life > 3)
        {
            playerState = PlayerState.Healthy;
        }
        else if (life > 2)
        {
            playerState = PlayerState.Injured;
        }
        else
        {
            playerState = PlayerState.Critical;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Entity enemy = collision.gameObject.GetComponent<Entity>();

            if (enemy != null)
            {
                if (isAssaulting)
                {
                    enemy.TakeDamage(assaultDamage);
                    isAssaulting = false;
                }
                else
                {
                    TakeDamage(enemy.damageAttack);
                }
            }
            else
            {
                Debug.LogError("⚠ Error: Enemy no tiene componente Entity.");
            }
        }

        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("TrapPlatform"))
        {
            isOnTrapPlatform = true;
        }

        if (collision.gameObject.CompareTag("Food"))
        {
            collision.gameObject.GetComponent<CollectableObject>().Collect();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("TrapPlatform"))
        {
            isOnTrapPlatform = false;
        }
    }

    private void Dead()
    {

        Time.timeScale = 0; 

        if (gamePlayCanvas != null)
        {
            gamePlayCanvas.onLose(); 
        }

        Destroy(gameObject, 1);
    }

    public void assaultGoat()
    {
        if (playerState == PlayerState.Critical || playerState == PlayerState.Injured)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isCharging = true;
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
                currentChargeTime = chargeTime;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            if (isCharging)
            {
                float shootForce = currentChargeTime / chargeTime * chargingSpeed * shootForceMultiplier;
                _rB.AddForce(transform.forward * shootForce, ForceMode.Impulse);
                isCharging = false;
                isAssaulting = true;
                currentChargeTime = 0.0f;
            }
        }
    }

    public void shootRock()
    {
        float cooldown = playerState == PlayerState.Critical ? increasedShootCooldown : defaultShootCooldown;

        if (Input.GetMouseButtonDown(0) && currentShootCooldown <= 0)
        {
            isShootingCharging = true;
            currentChargeTime = 0.0f;
        }

        if (Input.GetMouseButton(0) && isShootingCharging)
        {
            currentChargeTime += Time.deltaTime;
            if (currentChargeTime > chargeTime)
            {
                currentChargeTime = chargeTime;
            }
        }

        if (Input.GetMouseButtonUp(0) && isShootingCharging)
        {
            float shootForce = (currentChargeTime / chargeTime) * shootForceMultiplier;
            GameObject bullet = Instantiate(shootBall, shootPoint.position, shootPoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = shootPoint.forward * shootForce;

            ShootPlayer bulletScript = bullet.GetComponent<ShootPlayer>();
            if (currentChargeTime >= chargeTime)
            {
                bulletScript.damage *= 3;
            }

            isShootingCharging = false;
            currentChargeTime = 0.0f;
            currentShootCooldown = cooldown;
        }
    }
}

