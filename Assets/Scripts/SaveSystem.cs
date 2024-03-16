using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;

public static class SaveSystem
{
    static string fileName = "/player.inf";
    public static void SavePlayer (PlayerController player) {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + fileName;

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadPlayer() {

        string path = Application.persistentDataPath + fileName;

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
