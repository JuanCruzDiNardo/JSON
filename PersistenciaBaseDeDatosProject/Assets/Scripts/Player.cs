using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;
    public float health;
    public UiManager uiManager;
    // Eventos
    public static event Action<int, int> OnSave; // saveID añadido
    public static event Action<UiManager, int> OnLoad; // saveID añadido

    public void OnEnable()
    {
        OnSave += SaveGame;
        OnLoad += LoadGame;
    }

    public void OnDisable()
    {
        OnSave -= SaveGame;
        OnLoad -= LoadGame;
    }

   public  void SaveGame(int dropdownValue, int saveID)
    {
        SaveSystem.SavePlayer(this, dropdownValue, saveID);
        Debug.Log("Juego guardado con ID: " + saveID);
    }
   
    public void cargar()
    {
        
    }

    public void LoadGame(UiManager uiManager, int saveID)
    {
        PlayerData data = SaveSystem.LoadPlayer(saveID);

        if (data != null)
        {
            level = data.level;
            health = data.health;

            // Restaurar la posición del jugador
            Vector3 loadedPosition = new Vector3(data.positionX, data.positionY, data.positionZ);
            transform.position = loadedPosition;

            // Restaurar el valor del Dropdown
            uiManager.TMP_Dropdown.value = data.dropdownValue;

            Debug.Log("Juego cargado. Posición restaurada.");
        }
    }

    public static void TriggerSave(int dropdownValue, int saveID)
    {
        OnSave?.Invoke(dropdownValue, saveID);
    }

    public static void TriggerLoad(UiManager uiManager, int saveID)
    {
        OnLoad?.Invoke(uiManager, saveID);
    }
}