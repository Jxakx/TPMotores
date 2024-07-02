using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Golem : Entity
{
    public int routine;
    public float chronometer;
    public Quaternion angle;
    public float grade;
    public float speed;

    private float counter;
    public float timer;

    public float distanceToPlayer;

    public float distanceJumpAttack;
    public float distanceRockAttack;
    public float distancePunchAttack;

    public Transform punchArm;
    private bool isPunching;
    private Vector3 initialPunchArmPosition;

    public Transform pointRock;
    [SerializeField] public GameObject rock;
    [SerializeField] Transform puntoDeDisparo;

    public GameObject targetPlayer;

    void Start()
    {
        targetPlayer = GameObject.Find("PlayerPrueba");
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.transform.position);
        walk();
    }

    public void walk()
    {
        if(Vector3.Distance(transform.position, targetPlayer.transform.position) > visionRange)
        {
            chronometer += 1 * Time.deltaTime;

            if (chronometer >=  3)
            {
                routine = Random.Range(0, 2);
                chronometer = 0;
            }

            switch (routine)
            {
                case 1:
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grade, 0);
                    routine++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * speed * 1 * Time.deltaTime);

                    
                    break;
            }
        }
        else 
        {
            counter += Time.deltaTime;

            LookPlayer();

            transform.Translate(Vector3.forward * speed * 2  * Time.deltaTime);

            Vector3 directionToPlayer = (Player.position - transform.position).normalized;
            directionToPlayer.y = 0;

            Quaternion desiredRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, speed * Time.deltaTime);

            if (Physics.Raycast(puntoDeDisparo.position, puntoDeDisparo.forward, out RaycastHit hit, actionRange, detectableLayers))
            {

                if (hit.transform.CompareTag("Player"))
                {
                    if(distanceToPlayer <= distanceRockAttack && distanceToPlayer >= distancePunchAttack)
                    {
                        attackRock();
                        print("Tiro Roca");
                    }
                    else if (distanceToPlayer <= distancePunchAttack)
                    {
                        if (!isPunching)
                        {
                            StartCoroutine(PunchAttack());
                        }
                        print("Tiro puño");
                    }
                }

            }

        }

    }

    public void attackRock()
    {
        if(counter >= timer)
        {
            counter = 0;
            Instantiate(rock, pointRock.position, pointRock.rotation);
        }

    }

    private IEnumerator PunchAttack()
    {
        isPunching = true;

        // Posición inicial es la posición local actual del PunchArm
        Vector3 startPos = punchArm.localPosition;

        // Posición final es un poco adelante y ligeramente hacia el jugador
        Vector3 endPos = startPos + new Vector3(0, 0, 0.5f); // Ajusta este vector según sea necesario

        // Movimiento hacia adelante (apuñalamiento)
        float elapsedTime = 0f;
        while (elapsedTime < 1f) // Ajusta el tiempo para un movimiento más corto
        {
            punchArm.localPosition = Vector3.Lerp(startPos, endPos, (elapsedTime / 0.2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Movimiento de regreso
        elapsedTime = 0f;
        while (elapsedTime < 0.5f) // Ajusta el tiempo para un movimiento más corto
        {
            punchArm.localPosition = Vector3.Lerp(endPos, startPos, (elapsedTime / 0.2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        punchArm.localPosition = punchArm.localPosition; // Asegurar que el brazo vuelve a la posición inicial
        isPunching = false;
    }
}
