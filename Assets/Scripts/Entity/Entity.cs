using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int life;
    public int damageAttack;
    public int score;

    public int distanceRange; //Rango en el cual la entidad mirará al jugador

    public Vector3 directionTarget; //Distancia que ve al Player
    public Transform targetPlayer; //Para saber donde está el Player siempre


    void Start()
    {
        
    }

    void Update()
    {
        LookPlayer(); //Para que se actualice y siempre mire al objetivo.
        takeDamage();
        
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

    public void LookPlayer()
    {
        //Calcula distancia entre entidad y player
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

        if (distanceToPlayer < distanceRange)
        {
            //Calcula la dirección desde la posición actual de la entidad hacia la posición del Player
            directionTarget = (targetPlayer.position - transform.position).normalized;
            //Ajusta la orientación de la entidad para que mire en la dirección del Player
            transform.forward = directionTarget;
        }
        
    }
}
