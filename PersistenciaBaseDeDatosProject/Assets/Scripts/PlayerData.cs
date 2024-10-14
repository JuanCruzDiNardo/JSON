using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int level;
    public float health;
    public int dropdownValue; // Valor del Dropdown
    public int saveID; // ID de la partida guardada

    // Posición del jugador
    public float positionX;
    public float positionY;
    public float positionZ;

    public PlayerData(Player player, int dropdownValue, int saveID)
    {
        level = player.level;
        health = player.health;
        this.dropdownValue = dropdownValue;
        this.saveID = saveID;

        // Guardamos la posición del jugador
        positionX = player.transform.position.x;
        positionY = player.transform.position.y;
        positionZ = player.transform.position.z;
    }
}