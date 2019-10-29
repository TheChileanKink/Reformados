using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transitions : MonoBehaviour
{
    public GameObject flag;
    public Canvas canvas;
    bool played = false;

    public Collision2D bottom;
    bool FlagMoved = false;

    private float perc = 0;
    private int correctas = 0;

    private int total = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Saving.SaveData(10, 10);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // SceneManager.LoadScene("Pregunta");
            Saving.DeleteFile();
        }


        if (canvas.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("UIOUT") && !played)
        {
            StartCoroutine(ReadProgress());
            played = true;
        }

    }
    IEnumerator ReadProgress()
    {
        Debug.Log("Reading Progress...");
        PlayerData data = Saving.LoadData();
        correctas = data.completados;
        total = data.total;
        perc = data.per;
        Debug.Log(perc.ToString() + "%");
        Animator anim = canvas.GetComponent<Animator>();
        if (correctas == 0)
        {
            // Initial Explanation Scene Before Questions.
            Debug.Log("Opening Tutorial...");
            anim.SetTrigger("tutorial");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Pregunta");
        }
        else if (correctas == total)
        {
            Debug.Log("Juego Completado! Congrats.");
        }
        else
        {
            anim.SetTrigger("progress");
            // Lower Flag.
            // Show Percentage.
        }
    }
}
