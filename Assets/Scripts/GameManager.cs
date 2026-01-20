using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TPFinal - [Tu Nombre]
// Heredamos de Singleton<GameManager> para usar el genérico
public class GameManager : Singleton<GameManager>
{
    [Header("UI References")]
    public GameplayCanvasManager canvasManager; // Referencia al canvas que ya tienes

    // CUMPLIENDO REQUISITO: Diccionarios
    // Guardamos el nombre del item y la cantidad recogida
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    private void Start()
    {
        // Nos suscribimos a la muerte del jugador
        // Asegúrate de que Player.OnLifeChanged envíe 0 cuando muere o crea un evento OnDeath
    }

    // Método para agregar items al diccionario
    public void AddItemToInventory(string itemName, int amount = 1)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += amount;
        }
        else
        {
            inventory.Add(itemName, amount);
        }

        Debug.Log($"Item recolectado: {itemName}. Cantidad total: {inventory[itemName]}");
    }

    public void GameOver()
    {
        if (canvasManager != null) canvasManager.onLose();
    }

    public void Victory()
    {
        if (canvasManager != null) canvasManager.onWin();
    }
}