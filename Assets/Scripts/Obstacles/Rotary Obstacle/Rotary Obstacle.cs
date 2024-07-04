using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 Joaquin Lopez
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

    private void Death()
    {
        Destroy(gameObject);
    }

}
