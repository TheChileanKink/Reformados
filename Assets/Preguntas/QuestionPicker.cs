using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPicker : MonoBehaviour
{
    // Get JSON.
    // Get Progress.
    private string pregunta = "¿De Qué Color es el Mar?";
    private List<string> respuestas = new List<string> { "azul", "rojo", "verde", "morado" };
    private string fuente = "Art 30 Linea 1";
    private List<string> respondidas;
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    public Text title;
    private List<GameObject> Botones = new List<GameObject>();
    void Start()
    {
        string correcta = respuestas[0];
        title.text = pregunta;
        GetQuestion();
        // Find Not Answered Question.
        // Spawnables Details (Condor-Copihue) on Questions.

        for (int i = 0; i < respuestas.Count; i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);

            Button Boton = button.GetComponent<Button>();
            if (i == 0)
            {
                Boton.onClick.AddListener(Correct);
                Debug.Log("Respuesta Correcta: " + respuestas[0]);
            }
            else
            {
                Boton.onClick.AddListener(Incorrect);

            }
            button.transform.GetChild(0).GetComponent<Text>().text = respuestas[i].ToString();
            Botones.Add(button);
        }

        StartCoroutine(AddButtons());
    }
    void GetQuestion(){
        // Get JSON
        // Get Progress
        // Find Question Not Answered
        // Set Variables
    }
    IEnumerator AddButtons()
    {
        for (int i = 0; i < Botones.Count; i++)
        {
            GameObject temp = Botones[i];
            int randomIndex = Random.Range(i, Botones.Count);
            Botones[i] = Botones[randomIndex];
            Botones[randomIndex] = temp;
        }
        int count = 1;
            yield return new WaitForSeconds((float)0.7);
        foreach (GameObject Btn in Botones)
        {

            Btn.transform.position = new Vector3(Btn.transform.position.x, Btn.transform.position.y - count * 6 / 7, 1);
            Btn.transform.SetParent(panelToAttachButtonsTo.transform);
            Btn.GetComponent<Animator>().SetTrigger("in");
            count++;
            yield return new WaitForSeconds((float)0.6);

        }
    }
    void Correct()
    {
        Debug.Log("Correct");
        // Mark as Done.
        // Increase Progress %.
        // Maybe Add Coins.
        // Go to Next Scene.

    }
    void Incorrect()
    {
        Debug.Log("Incorrect");
        // Show Correct Answer.
        // Restart With New Question.
        
        GetQuestion();

    }
}
