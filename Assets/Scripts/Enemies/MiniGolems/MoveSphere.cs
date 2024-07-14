using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TP2 Santiago Rodriguez Barba 
public class MoveSphere : MonoBehaviour
{
    public int speed = 5;
    public Vector3 direction = new Vector3(0, 0, 0);
    public int damage = 1;
    private bool onlyOne = true;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.Rotate(new Vector3(1, 1, 1) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            if (onlyOne)
            {
                player.TakeDamage(damage);
                onlyOne = false;
            }
        }
    }
}
