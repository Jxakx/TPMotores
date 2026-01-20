using UnityEngine;

// TPFinal - Santiago Rodriguez Barba
public class CollectableObject : MonoBehaviour
{
    public Item collectableObject; 
    public Score scoreScript;     

    public void Collect()
    {
        // Sumar puntos 
        if (scoreScript != null) scoreScript.AddPoints(collectableObject.score);

        GameManager.Instance.AddItemToInventory(collectableObject.name);

        Destroy(gameObject);
    }
}