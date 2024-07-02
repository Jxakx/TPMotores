using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Entity
{
    
    [SerializeField] GameObject VenomBallPrefab;
    [SerializeField] int speedRotation;
    [SerializeField] int UpDownSpeed;
    [SerializeField] Transform shootingPoint;

    public float ShootTimer;
    private float _counter;
    [SerializeField] Vector3[] positions;
    [SerializeField] int index;

    void Start()
    {
        Player = FindObjectOfType<Player>().transform;
    }

    
    void Update()
    {
        bool playerInRange = Vector3.Distance(transform.position, Player.position) < visionRange;
        _counter += Time.deltaTime;

        if (playerInRange == true)
        {
            //print("Player in range");
            Vector3 directionToPlayer = (Player.position - transform.position).normalized;
            directionToPlayer.y = 0;

            Debug.DrawRay(transform.position, directionToPlayer * visionRange, Color.red);

            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, visionRange, detectableLayers))
            {

                if (hit.transform.CompareTag("Player"))
                {
                    //print("Te veo");
                    LookPlayer();
                }
                else
                {
                    //print("No te veo");
                    RotateSnake();
                }

            }
        }
        else
        {
            //print("Player out of range");
            RotateSnake();
            /*Vector3 directionToPosition = (positions[index] - transform.position).normalized;


            transform.position += directionToPosition * UpDownSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, positions[index]) < 0.1f)
            {
                index++;
            }
            if (index >= positions.Length)
            {
                index = 0;
            }*/
        }
    }

    private void MoveUpDown()
    {
        Vector3 directionToPosition = (positions[index] - transform.position).normalized;


        transform.position += directionToPosition * UpDownSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, positions[index]) < 0.1f)
        {
            index++;
        }
        if (index >= positions.Length)
        {
            index = 0;
        }
    }

    private void RotateSnake()
    {
        transform.Rotate(0, speedRotation * Time.deltaTime, 0);
    }

    public void SpitPoison()
    {
        if (_counter >= ShootTimer)
        {
            _counter = 0;
            Instantiate(VenomBallPrefab, shootingPoint.position, shootingPoint.rotation);
            //print("Te disparo");
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
