using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UI ui;
    private int hp = 10;

    public void SetHP(int newHP)
    {
        hp = newHP;
        ui.UpdateHP(hp);
    }

    public void LoseHP(int damage)
    {
        hp -= damage;
        if (hp < 0) hp = 0;
        ui.UpdateHP(hp);
    }

    public void GainHP(int amount)
    {
        hp += amount;
        if (hp > 10) hp = 10; // Suponiendo que 10 es el máximo
        ui.UpdateHP(hp);
    }

}
