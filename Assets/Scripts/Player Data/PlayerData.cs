using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData {

    public int completados;
    public int total;
    public float per;

    public List<string> answered;

    public PlayerData(int correctos,int cien,List<string> respondidas){
        completados=correctos;
        total=cien;
        answered=respondidas;
        if(cien!=0){
            per=correctos*100/cien;
        }else{
            per=0;
            Debug.Log("Error: Total no puede ser 0");
        }
    }
}
