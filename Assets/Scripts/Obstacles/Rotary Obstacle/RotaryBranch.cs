using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryBranch : MonoBehaviour
{
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
}
