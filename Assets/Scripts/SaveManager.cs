using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Enums;

public static class SaveManager
{
    private static readonly string gamePath = Application.persistentDataPath + "/game-data.json";

    public static void SaveGame()
    {
        string path = gamePath;
        GameData data = new GameData();
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(path, json);
        Debug.Log("Jeu sauvegardé à : " + path);
    }

    public static GameData LoadGame()
    {
        bool hasGameSave = File.Exists(gamePath);

        if (hasGameSave == false)
        {
            Debug.LogWarning("Aucune sauvegarde trouvée.");
            return null;
        }

        string path = "";
        if (hasGameSave == true)
        {
            path = gamePath;
        }

        string json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json))
        {
            Debug.LogWarning("Le fichier de sauvegarde est vide.");
            return null;
        }

        GameData data = JsonConvert.DeserializeObject<GameData>(json);
        return data;
    }

    public static void DeleteSave()
    {
        string path = gamePath;
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Sauvegarde supprimée.");
        }
        else
        {
            Debug.LogWarning("Aucune sauvegarde à supprimer.");
        }
    }

}
