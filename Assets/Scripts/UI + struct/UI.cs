using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject[] hearts; 
    public GameObject gameOverScreen; 

    private void OnEnable()
    {
        Player.OnLifeChanged += UpdateHP; //Se suscribe al evento
    }

    private void OnDisable()
    {
        Player.OnLifeChanged -= UpdateHP; //Se desuscribe al desactivarse
    }

    private void UpdateHP(int hp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < hp);
        }
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
