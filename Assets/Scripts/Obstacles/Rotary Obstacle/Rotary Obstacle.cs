using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryObstacle : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    [SerializeField] int speed;

    private void FixedUpdate()
    {
        Spin();
    }

    private void Spin()
    {
        Vector3 rote = new Vector3(0, 1, 0);

        this.transform.Rotate(rote * speed * Time.deltaTime);
    }

    public void takeDamage(int damage = 1)
    {
 
        hp -= damage;

        if (Input.GetKeyDown(KeyCode.S))
        {            
            hp-= 10; //en vez del 10, tiene que ir el int del daño del player            
        }

        if (hp <= 0)
        {
            Death();
        }


    }

    private void Death()
    {
        Destroy(gameObject);
    }

}
