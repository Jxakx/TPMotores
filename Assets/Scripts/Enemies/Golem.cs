using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public int routine;
    public float chronometer;
    public Quaternion angle;
    public float grade;
    public float speed;

    public int distance;

    public GameObject targetPlayer;

    void Start()
    {
        targetPlayer = GameObject.Find("PlayerPrueba");
    }

    void Update()
    {
        walk();
    }

    public void walk()
    {
        if(Vector3.Distance(transform.position, targetPlayer.transform.position) > distance)
        {
            chronometer += 1 * Time.deltaTime;

            if (chronometer > 4)
            {
                routine = Random.Range(0, 2);
                chronometer = 0;
            }

            switch (routine)
            {
                case 1:
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0f, grade, 0f);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
            var lookPos = targetPlayer.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

            transform.Translate(Vector3.forward * speed * 2 * Time.deltaTime);

        }

    }
}
