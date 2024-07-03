using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 Joaquin Lopez
public class Snake : Entity
{
    [SerializeField] GameObject VenomBallPrefab;
    
    [SerializeField] Transform shootingPoint;

    public float ShootTimer;
    private float _counter;

    protected override void Start()
    {
        Player = FindObjectOfType<Player>().transform;
    }

    protected override void Update()
    {
        _counter += Time.deltaTime;
        base.Update(); // Llama al Update de entity
        bool playerInRange = Vector3.Distance(transform.position, Player.position) < visionRange;
        if (!playerInRange)
        {
            RotateSnake();
        }
    }

    public override void LookPlayer()
    {
        base.LookPlayer(); // Llama al LookPlayer de entity
        SpitPoison(); 
    }

    private void RotateSnake()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void SpitPoison()
    {
        if (_counter >= ShootTimer)
        {
            _counter = 0;
            Instantiate(VenomBallPrefab, shootingPoint.position, shootingPoint.rotation);
            print("Te disparo");
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, actionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}

