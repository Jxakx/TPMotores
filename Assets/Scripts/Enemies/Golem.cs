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

    public float jumpForce = 5.0f; // Fuerza con la que el cubo salta hacia arriba
    public float fallSpeed = 10.0f; // Velocidad a la que el cubo vuelve al suelo
    private Vector3 originalPosition; // Posici�n original del cubo
    private bool isJumping = false; // Flag para controlar si el cubo est� saltando

    public Transform punchArm;
    private bool isPunching;
    private Vector3 initialPunchArmPosition;

    public Transform pointRock;
    [SerializeField] public GameObject rock;
    [SerializeField] Transform puntoDeDisparo;

    public GameObject targetPlayer;

    void Start()
    {
        originalPosition = transform.position;

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
                    if(distanceToPlayer <= distanceJumpAttack && distanceToPlayer >= distanceRockAttack && distanceToPlayer >= distancePunchAttack)
                    {
                        int timeJump = Random.Range

                        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
                        {
                            StartCoroutine(JumpAndFall());
                        }

                    }
                    else if(distanceToPlayer <= distanceRockAttack && distanceToPlayer >= distancePunchAttack)
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
                        print("Tiro pu�o");
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

        // Posici�n inicial es la posici�n local actual del PunchArm
        Vector3 startPos = punchArm.localPosition;

        // Posici�n final es un poco adelante y ligeramente hacia el jugador
        Vector3 endPos = startPos + new Vector3(0, 0, 0.5f); // Ajusta este vector seg�n sea necesario

        // Movimiento hacia adelante (apu�alamiento)
        float elapsedTime = 0f;
        while (elapsedTime < 1f) // Ajusta el tiempo para un movimiento m�s corto
        {
            punchArm.localPosition = Vector3.Lerp(startPos, endPos, (elapsedTime / 0.2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Movimiento de regreso
        elapsedTime = 0f;
        while (elapsedTime < 0.5f) // Ajusta el tiempo para un movimiento m�s corto
        {
            punchArm.localPosition = Vector3.Lerp(endPos, startPos, (elapsedTime / 0.2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        punchArm.localPosition = punchArm.localPosition; // Asegurar que el brazo vuelve a la posici�n inicial
        isPunching = false;
    }


    IEnumerator JumpAndFall()
    {
        isJumping = true;

        // Salto r�pido hacia arriba
        Vector3 jumpTarget = transform.position + Vector3.up * jumpForce;
        while (transform.position.y < jumpTarget.y)
        {
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime, Space.World);
            yield return null;
        }

        // Ca�da suave de vuelta al suelo
        while (transform.position.y > originalPosition.y)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        transform.position = originalPosition;
        isJumping = false;
    }
}
