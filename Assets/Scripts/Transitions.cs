using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transitions : MonoBehaviour {
    public GameObject flag;
    public Canvas canvas;
    bool played = false;

    public Collision2D bottom;
    bool FlagMoved = false;

    private float perc = 0;
    private int correctas = 0;

    private int total = 0;
    private List<string> answ = new List<string>{};

    private Questions questionsCollection;

    void Start() {
        questionsCollection=DataBase.ReadDB();
        total = DataBase.questionCollection.preguntas.Length;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Saving.SaveData(total, total, new List<string>{});
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Saving.SaveData(Random.Range(0, total), total, new List<string>{});
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Saving.SaveData(0, total, new List<string>{});
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Saving.DeleteFile();
        }


        if (canvas.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("UIOUT") && !played) {
            StartCoroutine(ReadProgress());
            played = true;
        } 
    }
    IEnumerator ReadProgress() {
        Debug.Log("Reading Progress...");


        total = DataBase.questionCollection.preguntas.Length;

        PlayerData data = Saving.LoadData();
        correctas = data.completados;
        total = data.total;
        perc = data.per;
        answ = data.answered;
        Debug.Log(perc.ToString() + "%");

        Animator anim = canvas.GetComponent<Animator>();
        if (correctas == 0) {
            // Initial Explanation Scene Before Questions.
            Debug.Log("Opening Tutorial...");
            anim.SetTrigger("tutorial");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Pregunta");
        } else if (correctas == total) {
            Debug.Log("Juego Completado! Congrats.");
        } else {
            anim.SetTrigger("progress");
            SceneManager.LoadScene("Progreso");
        }
    }
}
