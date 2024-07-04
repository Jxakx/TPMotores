using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public Item collectableObject;
    public Score scoreScript;

    public void Collect()
    {
        scoreScript.addPoints(collectableObject.score);
        Destroy(gameObject);
    }
}
