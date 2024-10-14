using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string savePath = Application.persistentDataPath + "/save";
    //private static readonly string listPath = Application.persistentDataPath + "/savesList.save";
    private static readonly string listPath = Application.persistentDataPath + "/savesList.json";

    // Guardar una partida
    public static void SavePlayer(Player player, int dropdownValue, int saveID)
    {
        //BinaryFormatter formatter = new BinaryFormatter();
        //string path = savePath + saveID + ".save";
        //FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerData data = new PlayerData(player, dropdownValue, saveID);
        //formatter.Serialize(stream, data);
        //stream.Close();

        string path = savePath + saveID + ".json";
        PlayerData data = new PlayerData(player, dropdownValue, saveID);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(path, json);

        SaveSavesList(saveID); // Guardar la lista de partidas
    }

    // Cargar una partida
    public static PlayerData LoadPlayer(int saveID)
    {
        //string path = savePath + saveID + ".save";
        string path = savePath + saveID + ".json";
        if (File.Exists(path))
        {
            //BinaryFormatter formatter = new BinaryFormatter();
            //FileStream stream = new FileStream(path, FileMode.Open);

            //PlayerData data = formatter.Deserialize(stream) as PlayerData;
            //stream.Close();

            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            return data;
        }
        else
        {
            Debug.LogError("Archivo de guardado no encontrado en " + path);
            return null;
        }
    }

    // Guardar la lista de IDs de las partidas
    private static void SaveSavesList(int saveID)
    {
        List<int> savesList = LoadSavesList() ?? new List<int>();
        if (!savesList.Contains(saveID))
        {
            savesList.Add(saveID);
            if (savesList.Count > 5) // Limitar a 10 partidas
            {

                DeleteSaveFile(savesList[0]);

                savesList.RemoveAt(0);


            }
        }

        string json = JsonUtility.ToJson(new  SaveListWrapper(savesList));

        File.WriteAllText(listPath, json);

        //BinaryFormatter formatter = new BinaryFormatter();
        //FileStream stream = new FileStream(listPath, FileMode.Create);
        //formatter.Serialize(stream, savesList);
        //stream.Close();
    }

    // Cargar la lista de IDs de partidas guardadas
    public static List<int> LoadSavesList()
    {
        if (File.Exists(listPath))
        {
            //BinaryFormatter formatter = new BinaryFormatter();
            //FileStream stream = new FileStream(listPath, FileMode.Open);

            //List<int> savesList = formatter.Deserialize(stream) as List<int>;
            //stream.Close();

            string json  = File.ReadAllText(listPath);

            SaveListWrapper wrapper = JsonUtility.FromJson<SaveListWrapper>(json);

            //return savesList;
            return wrapper.savesList;
        }
        else
        {
            return new List<int>(); // Si no existe el archivo, devolver una lista vacía
        }
    }

    private static void DeleteSaveFile(int saveID)
    {
        // Construir la ruta del archivo correspondiente
        string path = savePath + saveID + ".json";

        // Verificar si el archivo existe
        if (File.Exists(path))
        {
            // Borrar el archivo
            File.Delete(path);
            Debug.Log("Archivo eliminado: " + path);
        }
        else
        {
            Debug.LogWarning("Intento de eliminar archivo, pero no se encontró: " + path);
        }
    }

    public static void DeleteSaveList()
    {
        List<int> savesList = LoadSavesList() ?? new List<int>();
        if (savesList.Count > 0)
        {
            for (int i = 0; i > savesList.Count; i++)
            {
                Debug.LogWarning(savesList[i]);
                DeleteSaveFile(savesList[i]);
            }
            File.Delete(listPath);
        }
    }

    // Clase auxiliar para envolver la lista de saves
    [System.Serializable]
    private class SaveListWrapper
    {
        public List<int> savesList;

        public SaveListWrapper(List<int> savesList)
        {
            this.savesList = savesList;
        }
    }
}