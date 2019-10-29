using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData {

    public int completados;
    public int total;
    public float per;

    public int[] answered;

    public PlayerData(int correctos,int cien){
        completados=correctos;
        total=cien;
        if(cien!=0){
            per=correctos*100/cien;
        }else{
            per=0;
            Debug.Log("Error: Total=0");
        }
    }
}
