using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    public GameObject flag;
    public Canvas canvas;
    bool played=false;

    public Collision2D bottom;

    void Update()
    {
        if (canvas.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Second") && !played) { 
            played=true;
            MoveFlag();
        }

    }
    void MoveFlag(){
        
    }
}
