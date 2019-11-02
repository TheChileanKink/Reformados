using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DataBase
{
        public static Questions questionCollection=null;
    public static Questions ReadDB()
    {
        string path;
        string jsonString;
        path = Application.streamingAssetsPath + "/preguntas.json";

        using (StreamReader stream = new StreamReader(path))
        {
            jsonString = stream.ReadToEnd();
            // Debug.Log(jsonString);
            questionCollection = JsonUtility.FromJson<Questions>(jsonString);
        }
        Debug.Log("Question Loaded :" + questionCollection.preguntas.Length.ToString());
        return questionCollection;
    }}

    [System.Serializable]
    public class Pregunta
    {
        public string id;
        public string creador;
        public string pregunta;
        public string[] respuestas;
        public string fuente;
    }
    [System.Serializable]
    public class Questions
    {
        public Pregunta[] preguntas;
    }
