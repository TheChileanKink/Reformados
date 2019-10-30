using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPicker : MonoBehaviour
{
    // Get JSON.
    // Get Progress.
    private string pregunta = "¿De Qué Color es el Mar?";
    private List<string> respuestas = new List<string> { "Azul es el color del Mar", "Rojo es el color del Mar", "Verde es el color del Mar", "Morado es el color del Mar" };
    private string fuente = "Art 30 Linea 1";
    private List<string> respondidas;
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    public Text title;
    private List<GameObject> Botones = new List<GameObject>();
    public Font estiloTexto;
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
                Boton.onClick.AddListener(() => { StartCoroutine(Correct(button)); });
                Debug.Log("Respuesta Correcta: " + respuestas[0]);
            }
            else
            {
                Boton.onClick.AddListener(Incorrect);

            }
            Text texto =button.transform.GetChild(0).GetComponent<Text>();
            texto.text = respuestas[i].ToString();
            texto.font= estiloTexto;
            // texto.color=new Color(0,0,0,1);
            Botones.Add(button);
        }

        StartCoroutine(AddButtons());
    }
    void GetQuestion()
    {
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
            Btn.GetComponent<Animator>().enabled=true;
            count++;
            yield return new WaitForSeconds((float)0.6);

        }
    }
    IEnumerator Correct(GameObject btn)
    {
        Animator correctAnimator = btn.GetComponent<Animator>();
        correctAnimator.SetTrigger("correct");
        Debug.Log("Correct");
        yield return new WaitForSeconds(1);

        // foreach (GameObject boton in Botones)
        // {
        //     if(boton==btn){
        //          correctAnimator.SetTrigger("hide");
        //     }else{
        //     Animator anim = boton.GetComponent<Animator>();
        //     anim.SetTrigger("hide"); 
        //     }
        //     yield return new WaitForSeconds((float)0.6);

        // }


    }
    void Incorrect()
    {
        Debug.Log("Incorrect");
        // Show Correct Answer.
        // Restart With New Question.

        GetQuestion();

    }
}
