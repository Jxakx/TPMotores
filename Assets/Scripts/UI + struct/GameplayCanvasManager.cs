using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TP2 Joaquin Lopez
public class GameplayCanvasManager : MonoBehaviour
{
    public GameObject losePanel, winPanel;
    
    void Start()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);

        Golem golem = FindObjectOfType<Golem>(); // Busca el golem en la escena
        if (golem != null)
        {
            golem.OnGolemDeath += HandleGolemDeath;
        }
    }


    public void onLose()
    {
        losePanel.SetActive(true);
        winPanel.SetActive(false);

    }

    public void onWin()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(true);

    }

    private void HandleGolemDeath()
    {
        Time.timeScale = 0; // Detener el tiempo cuando el golem muere
        winPanel.SetActive(true); // Mostrar el panel de victoria
    }
}
