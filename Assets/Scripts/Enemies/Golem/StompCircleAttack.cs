using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompCircleAttack : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(2);
            Destroy(gameObject);
        }
    }
}
