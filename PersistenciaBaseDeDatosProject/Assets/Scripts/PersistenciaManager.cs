using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersistenciaManager : MonoBehaviour
{
    public UiManager uiManager;
    public TMP_Dropdown saveDropdown;
    public Button saveButton;
    public Button loadButton;
    public Button cleanButton;

    private List<int> savesList;

    private void Start()
    {
        // Cargar las partidas guardadas al inicio y llenar el Dropdown
        savesList = SaveSystem.LoadSavesList();
        UpdateDropdown();

        // Configurar eventos de los botones
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        loadButton.onClick.AddListener(OnLoadButtonClicked);
        cleanButton.onClick.AddListener(cleanButtonClicked);

        if (savesList.Count > 0)
        {
            Player.TriggerLoad(uiManager, savesList[savesList.Count - 1]); // Cargar la última partida
        }
    }

    private void cleanButtonClicked()
    {
        SaveSystem.DeleteSaveList();
        savesList = SaveSystem.LoadSavesList(); // Actualizar la lista de partidas guardadas
        UpdateDropdown();
    }

    // Llamado al hacer clic en el botón de guardar
    private void OnSaveButtonClicked()
    {
        int saveID;

        if (savesList.Count > 0)
            saveID = savesList[savesList.Count - 1] + 1; // Generar nuevo ID para la partida        
        else
            saveID = 1;


        Player.TriggerSave(uiManager.TMP_Dropdown.value, saveID);
        savesList = SaveSystem.LoadSavesList(); // Actualizar la lista de partidas guardadas
        UpdateDropdown();
    }

    // Llamado al hacer clic en el botón de cargar
    private void OnLoadButtonClicked()
    {
        int selectedSaveID = savesList[saveDropdown.value];
        Player.TriggerLoad(uiManager, selectedSaveID);
    }

    // Actualizar el Dropdown con las partidas guardadas
    private void UpdateDropdown()
    {
        saveDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (int saveID in savesList)
        {
            options.Add("Partida " + saveID);
        }

        saveDropdown.AddOptions(options);
    }
}