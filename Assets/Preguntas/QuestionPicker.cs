using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class QuestionPicker : MonoBehaviour {
    // Get JSON.
    // Get Progress.
    // private string pregunta = "¿De Qué Color es el Mar?";
    // private List<string> respuestas = new List<string> { "Azul", "Rojo", "Verde", "Morado" };
    // private string fuente = "Art 30 Linea 1";
    // private List<string> respondidas;
    public Pregunta question;
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    public Text title;
    private List<GameObject> Botones = new List<GameObject>();
    public Font estiloTexto;
    public Animator sceneManager;

    private PlayerData data;
    private int total = DataBase.questionCollection.preguntas.Length;
    void Awake() {
        GetNextQuestion();

    }
    void Start() {

        string correcta = question.respuestas[0];
        title.text = question.pregunta;
        // Find Not Answered Question.
        // Spawnables Details (Condor-Copihue) on Questions.

        Animator correctAnimator = null;
        for (int i = 0; i < question.respuestas.Count(); i++) {
            GameObject button = (GameObject)Instantiate(buttonPrefab);

            Button Boton = button.GetComponent<Button>();
            if (i == 0) {
                correctAnimator = button.GetComponent<Animator>();
                Boton.onClick.AddListener(() => { StartCoroutine(Correct(correctAnimator)); });
                Debug.Log("Respuesta Correcta: " + question.respuestas[0]);
            } else {
                Animator incorrectAnimator = button.GetComponent<Animator>();
                Boton.onClick.AddListener(() => StartCoroutine(Incorrect(correctAnimator, incorrectAnimator)));

            }
            Text texto = button.transform.GetChild(0).GetComponent<Text>();
            texto.text = question.respuestas[i].ToString();
            texto.font = estiloTexto;
            // texto.color=new Color(0,0,0,1);
            Botones.Add(button);
        }

        StartCoroutine(AddButtons());
    }
    void GetNextQuestion() {
        data = Saving.LoadData();
        if(DataBase.questionCollection==null){
            DataBase.ReadDB();
        }
        int random = Random.Range(0, DataBase.questionCollection.preguntas.Length);
        Pregunta selected = DataBase.questionCollection.preguntas[random];

        if (data.answered.Contains(selected.id)) {
            // If Question has been answered
            Debug.Log("Already Answered: " + selected.id);
            if (data.answered.Count() == DataBase.questionCollection.preguntas.Count()) {
                SceneManager.LoadScene("Inicio");
                Debug.Log("Juego Finalizado. Congrats");

            } else {
                GetNextQuestion();
            }
        } else {
            question = selected;
            Debug.Log("Selected: " + question.id);
        }




        // Get Progress
        // Find Question Not Answered
        // Set Variables
    }
    IEnumerator AddButtons() {
        for (int i = 0; i < Botones.Count; i++) {
            GameObject temp = Botones[i];
            int randomIndex = Random.Range(i, Botones.Count);
            Botones[i] = Botones[randomIndex];
            Botones[randomIndex] = temp;
        }
        int count = 1;
        yield return new WaitForSeconds((float)0.7);
        foreach (GameObject Btn in Botones) {
            Btn.transform.position = new Vector3(Btn.transform.position.x, Btn.transform.position.y - count * 6 / 7, 1);
            Btn.transform.SetParent(panelToAttachButtonsTo.transform);
            Btn.GetComponent<Animator>().enabled = true;
            count++;
            yield return new WaitForSeconds((float)0.6);

        }
    }
    IEnumerator Correct(Animator correctAnimator) {
        correctAnimator.SetTrigger("correct");
        Debug.Log("Correct");
        yield return new WaitForSeconds((float)1);

        List<string> current = data.answered;
        current.Add(question.id);
        Saving.SaveData(data.completados + 1, total, current);
        if (data.completados + 1 == total) {
            Debug.Log("Game Ended!, Congrats.");
            StartCoroutine(HideAll("Inicio"));
        } else {
            StartCoroutine(HideAll("Pregunta"));
        }




    }
    IEnumerator HideAll(string scene) {
        foreach (GameObject boton in Botones) {
            Animator anim = boton.GetComponent<Animator>();
            anim.SetTrigger("hide");
            yield return new WaitForSeconds((float)0.3);
        }
        yield return new WaitForSeconds(1);
        sceneManager.SetTrigger("salida");

        foreach (GameObject boton in Botones) {
            Destroy(boton);
        }
        yield return new WaitForSeconds((float)1.3);
        SceneManager.LoadScene(scene);

    }
    IEnumerator Incorrect(Animator correctAnimator, Animator incorrectAnimator) {
        incorrectAnimator.SetTrigger("incorrect");
        yield return new WaitForSeconds((float)1);
        correctAnimator.SetTrigger("correct");
        yield return new WaitForSeconds((float)1);
        StartCoroutine(HideAll("Pregunta"));
    }
}
