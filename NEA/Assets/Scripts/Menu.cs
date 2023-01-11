using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject start, pause;

    bool isPaused = false; 

    public static bool stopPhysics;

    private void Start(){
        pause.gameObject.SetActive(false);
    }

    private void Update(){
        if(!isPaused){
            pause.gameObject.SetActive(false);
            start.gameObject.SetActive(true);
            stopPhysics = false;
        }else{
            start.gameObject.SetActive(false);
            pause.gameObject.SetActive(true);
        }
    }

    public void Start_Pause(){
        isPaused = !isPaused;
    }
}
