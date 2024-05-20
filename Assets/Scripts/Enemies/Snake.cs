using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] int life = 20;
    [SerializeField] GameObject VenomBallPrefab;
    [SerializeField] int turningSpeed;
    [SerializeField] int UpDownSpeed;
    [SerializeField] Transform shootingPoint;

    public float ShootTimer;
    private float _counter;
    [SerializeField] Vector3[] positions;
    [SerializeField] int index;

    void Start()
    {
        
    }

    
    void Update()
    {
        
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
        transform.Rotate(0, turningSpeed * Time.deltaTime, 0);
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
}
