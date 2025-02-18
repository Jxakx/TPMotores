using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TP2 Santiago Rodriguez Barba
public class CollectableObject : MonoBehaviour
{
    public Item collectableObject;
    public Score scoreScript;

    public void Collect()
    {
        scoreScript.AddPoints(collectableObject.score);
        Destroy(gameObject);
    }
}
