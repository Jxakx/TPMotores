using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrapPlatform: MonoBehaviour
{
    public List<GameObject> trapPlatform;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnJump += Activate;
        
    }

    private void Activate (object sender, EventArgs eventArgs)
    {
        foreach (GameObject item in trapPlatform)
        {
            item.SetActive(!item.activeSelf);
        }
    }
}
