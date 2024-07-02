using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UI ui;

    private int hp = 10;
   

    public void loseHP()
    {
        hp -= 1;
        ui.deactivateHP(hp);
    }

    public void gainHP()
    {
        hp += 1;
        ui.activateHP(hp);
    }



    
}
