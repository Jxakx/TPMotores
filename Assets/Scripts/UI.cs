using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject[] hp;

    
    public void deactivateHP(int indice)
    {
        if (indice <= 0) return;
        hp[indice].SetActive(false);
    }

    public void activateHP(int indice)
    {
        hp[(indice - 1)].SetActive(true);
    }
}
