using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 Joaquin Lopez
public class Spike : MonoBehaviour
{
    private int damage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
