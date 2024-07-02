using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    
    void Update()
    {
        transform.position = player.transform.position;
    }
}
