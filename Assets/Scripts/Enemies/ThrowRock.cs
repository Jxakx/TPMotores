using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float timeLife;
    [SerializeField] private int damage;

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

        PlayerPrueba player = collision.gameObject.GetComponent<PlayerPrueba>();
        if (player != null)
        {
            Destroy(this.gameObject);
            print("me destrui");
        }

    }

}
