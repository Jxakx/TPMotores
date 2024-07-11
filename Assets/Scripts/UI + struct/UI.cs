using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 Santiago Rodriguez Barba
public class UI : MonoBehaviour
{
    public GameObject[] hearts;

    public void UpdateHP(int hp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
            Debug.Log("Heart " + i + " is " + (hearts[i].activeSelf ? "active" : "inactive"));
        }
    }

    public void DeactivateHP(int index)
    {
        if (index >= 0 && index < hearts.Length)
        {
            hearts[index].SetActive(false);
        }
    }

    public void ActivateHP(int index)
    {
        if (index >= 0 && index < hearts.Length)
        {
            hearts[index].SetActive(true);
        }
    }
}
