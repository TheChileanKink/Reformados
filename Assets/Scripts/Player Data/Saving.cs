using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Saving
{

    public static PlayerData SaveData(int correctas, int total, List<string> respondidas)
    {
        string path = Application.persistentDataPath + "/informacion.fun";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(correctas, total,respondidas);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved "+correctas.ToString()+"/"+total.ToString());
        return data;

    }
    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/informacion.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;


        }
        else
        {
            
            Debug.Log("First Save.");
            PlayerData data = SaveData(0, DataBase.questionCollection.preguntas.Length, new List<string>{});
            return data;

        }
    }

    public static void DeleteFile()
    {
        string filePath = Application.persistentDataPath + "/informacion.fun";
        if (!File.Exists(filePath))
        {
            Debug.Log("NO FILE");
        }
        else
        {
            Debug.Log("File Deleted");

            File.Delete(filePath);
            // UnityEditor.AssetDatabase.Refresh();

        }
    }

}
