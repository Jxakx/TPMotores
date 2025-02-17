using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UI ui;
    private int hp = 5;

    public delegate void HPChanged(int newHP);
    public static event HPChanged OnHPChanged;

    private void Start()
    {
        OnHPChanged?.Invoke(hp); 
    }

    public void SetHP(int newHP)
    {
        hp = newHP;
        if (hp < 0) hp = 0;
        OnHPChanged?.Invoke(hp); 
    }

    public void LoseHP(int damage)
    {
        hp -= damage;
        if (hp < 0) hp = 0;

        Debug.Log("⚠ Vida actual: " + hp);
        OnHPChanged?.Invoke(hp); 

        if (hp == 0)
        {
            PlayerDie();
        }
    }

    public void GainHP(int amount)
    {
        hp += amount;
        if (hp > 10) hp = 10;
        OnHPChanged?.Invoke(hp);
    }

    private void PlayerDie()
    {
        ui.ShowGameOverScreen(); 
    }
}
