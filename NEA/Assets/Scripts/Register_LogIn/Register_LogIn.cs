using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register_LogIn : MonoBehaviour
{
    string username;//Stores username
    string password;//Stores password
    string rePass;//Stores reentered password

    public Button Enter;//Stores button
    public GameObject textBox;

    [Header("Text Boxes")]
    public InputField inUser; //Takes and stores input from textbox
    public InputField inPass;
    public InputField reInPass;

    private int strengthScore;


    private void Start() {
        textBox.SetActive(false);
    }

    public void getUsername(){
        //Debug.Log(inUser.text);

        username = inUser.text;
    }

    public void getPassword(){
        //Debug.Log(inPass.text);

        password = inPass.text;
    }

    public void getRePassword(){
        //Debug.Log(reInPass.text);

        rePass = reInPass.text;
    }

    public void getText(){ //Button will call functions and will store the text in boxes into strings
        getUsername();
        getRePassword();
        getPassword();

        // Debug.Log(username);
        // Debug.Log(password);
        // Debug.Log(rePass);

        checkPasswordEquality();
    }

    void checkPasswordEquality(){
        if(password == rePass){
            textBox.SetActive(false);
            checkPassStrength();
        }
        else{
            textBox.SetActive(true);
        }
    }

    void checkPassStrength(){
        Debug.Log("Checking Password");

    }
}