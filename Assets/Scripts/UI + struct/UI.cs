using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject[] hearts; 
    public GameObject gameOverScreen; 

    private void OnEnable()
    {
        GameManager.OnHPChanged += UpdateHP; // Se suscribe al evento de vida
    }

    private void OnDisable()
    {
        GameManager.OnHPChanged -= UpdateHP; // Se desuscribe cuando se desactiva
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
