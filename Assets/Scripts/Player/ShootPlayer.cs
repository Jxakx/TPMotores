using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeLife;
    [SerializeField] public int damage;

    void Start()
    {
        Destroy(gameObject, timeLife);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {        
        Entity entity = collision.gameObject.GetComponent<Entity>();

        if(entity != null)
        {
            entity.takeDamage(damage);
            Destroy(gameObject);
        }

    }

}
