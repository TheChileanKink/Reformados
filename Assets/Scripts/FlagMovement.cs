using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagMovement : MonoBehaviour {
    private bool goUp = true;
    private PlayerData data;
    float altura = 0;

    public Text progreso;
    void Start() {
        data = Saving.LoadData();
        altura = (float)data.per * (float)0.01 * 560 * 2 - 560;
        Debug.Log(data.per.ToString()+"%");
        Debug.Log("Altura: " + altura.ToString());

        progreso.text="¡"+data.per.ToString()+"% Completado!";
    }

    void Update() {
        if (altura < transform.localPosition.y&&goUp) {
            transform.Translate(new Vector3(0, 0, 0));
            goUp = false;
        }
        if (goUp) {
            transform.Translate(Vector3.up * Time.deltaTime, Space.World);
        }
    }
}
