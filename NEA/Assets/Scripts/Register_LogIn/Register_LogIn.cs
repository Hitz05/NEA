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

    [Header("Information Text")]
    public GameObject textBox;
    public GameObject checkButton;
    public GameObject imSure;

    [Header("Text Boxes")]
    public InputField inUser; //Takes and stores input from textbox
    public InputField inPass;
    public InputField reInPass;

    private int strengthScore = 0;

    bool checking = true;

    private void Start() {
        textBox.SetActive(false);
        checkButton.SetActive(false);
        imSure.SetActive(false);
    }

    public void agree(){
        checking = true;
        imSure.SetActive(false);
    }

    public void getUsername(){
        Debug.Log(inUser.text);

        username = inUser.text;
    }

    public void getPassword(){
        Debug.Log(inPass.text);

        password = inPass.text;
    }

    public void getRePassword(){
        Debug.Log(reInPass.text);

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
        checking = false;

        Debug.Log("Checking Password, please wait.....");

        int passwordLength = password.Length;
        int usernameLength = username.Length;

        char[] passChar = new char[passwordLength];
        char[] userChar = new char[usernameLength];

        //Checks if username and password are the same thing
        if(username == password){
            Debug.Log("Password and username are the same so security will be low. Are you sure you want to continue?");
            imSure.SetActive(true);
            checkButton.SetActive(true);
        }

        while(checking){
            for (int i = 0; i < passwordLength; i++){
                passChar[i] = password[i];

                int passASCII = System.Convert.ToInt32(passChar[i]);

                for (int ASCII_Code = 33; ASCII_Code < 48; ASCII_Code++)
                {
                    if(passASCII == ASCII_Code){
                        strengthScore++;
                    }
                }

                for (int ASCII_Code = 58; ASCII_Code < 65; ASCII_Code++)
                {
                    if(passASCII == ASCII_Code){
                        strengthScore++;
                    }
                }

                for (int ASCII_Code = 91; ASCII_Code < 97; ASCII_Code++)
                {
                    if(passASCII == ASCII_Code){
                        strengthScore++;
                    }
                }

                for (int ASCII_Code = 123; ASCII_Code < 127; ASCII_Code++)
                {
                    if(passASCII == ASCII_Code){
                        strengthScore++;
                    }
                }
            }

            strengthScore += passwordLength / 2;

            checking = false;
        }

        //Debug.Log(strengthScore);
    }
}