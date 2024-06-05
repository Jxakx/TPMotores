using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int life;
    public int damageAttack;
    public int score;

    public int visionRange; //Rango en el cual la entidad mirará al jugador
    public int actionRange;
    public float rotationSpeed; //Velocidad de rotación de la entidad

    public Vector3 directionTarget; //Distancia que ve al Player
    public Transform Player; //Para saber donde está el Player siempre
    //Para tener el transform de la entidad, basta con colocar transfrom.position.

    public LayerMask detectableLayers;

    void Start()
    {
        
    }

    void Update()
    {
        bool playerInRange = Vector3.Distance(transform.position, Player.position) < visionRange; //Distancia entre el enemigo y el jugador. 
        
        if(playerInRange == true)
        {
            print("Jugador dentro del rango");
            Vector3 directionToPlayer = (Player.position-transform.position).normalized; //Calcula la dirección. 
            directionToPlayer.y = 0;

            Debug.DrawRay(transform.position, directionToPlayer * visionRange, Color.red); 

            if(Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit,visionRange,detectableLayers)) //Rayito real si ve o no al player.
            {
                if (hit.transform.CompareTag("Player"))
                {
                    print("Te veo");
                    LookPlayer(); 
                }
                else
                {
                    print("No te veo");
                }
            }

        }
        else
        {
            print("Jugador fuera del rango");
        }

        takeDamage();
                
    }

    public void LookPlayer()
    {
        Vector3 directionToPlayer = (Player.position - transform.position).normalized; //Calcula la dirección. 
        directionToPlayer.y = 0;

        Quaternion desiredRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime); //Girar al jugador.

        Debug.DrawRay(transform.position, transform.forward * actionRange, Color.blue); //Esta vez, al entrar en el rango de acción, se puede ejecutar tal cosa, ej, disparar.

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, actionRange, detectableLayers)) //Rayito real si ve o no al player.
        {
            if (hit.transform.CompareTag("Player"))
            {
                print("Te veo");
            }
            else
            {
                print("No te veo");
            }
        }

    }

    public void takeDamage()
    {
        //Tener el int del daño que hará el PJ para colocarlo aquí.

        //life -= (int del daño del pj)

        
        if (Input.GetKeyDown(KeyCode.S))
        {            
            life -= 10; //en vez del 10, tiene que ir el int del daño del player            
        }

        if (life <= 0)
        {
            Death();
        }
       

    }

    public void Death()
    {
        Destroy(gameObject);
    }

    
    
}
