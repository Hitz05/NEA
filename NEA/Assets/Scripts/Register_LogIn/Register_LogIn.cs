using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

public class Register_LogIn : MonoBehaviour
{
    string username;//Stores username
    string password;//Stores password
    string rePass;//Stores reentered password

    public Button Enter;//Stores button
    public Button Register;

    [Header("Information Text")] //Stores the text boxes
    public GameObject textBox;
    public GameObject checkButton;
    public GameObject imSure;

    [Header("Text Boxes")] //Takes and stores text input
    public InputField inUser;
    public InputField inPass;
    public InputField reInPass;

    private int strengthScore = 0; //Stores strength of password

    bool needsChecking = true;
    bool alrChecked = false;

    private void Start() {
        Register.gameObject.SetActive(false);

        textBox.SetActive(false);
        checkButton.SetActive(false);
        imSure.SetActive(false);
    }

    public void agree(){
        imSure.SetActive(false);
        needsChecking = false;
        alrChecked = true;
        checkButton.SetActive(false);
        Register.gameObject.SetActive(true);
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
            checkPassStrength(0);
        }
        else{
            textBox.SetActive(true);
        }
    }

    void checkPassStrength(int score){
        strengthScore = 0;

        Debug.Log("Checking Password, please wait.....");

        int passwordLength = password.Length; //Stores how many characters there are in the password as an integer

        char[] passChar = new char[passwordLength];//Creates an array with as big as the length of the password

        //Checks if username and password are the same thing
        while(username == password && !alrChecked){
            needsChecking = true;
            Debug.Log("Password and username are the same so security will be low. Are you sure you want to continue?");
            imSure.SetActive(true);
            checkButton.SetActive(true);

            break;
        }

        Debug.Log(needsChecking);

        while(!needsChecking){
            for (int i = 0; i < passwordLength; i++){
                passChar[i] = password[i];

                int passASCII = System.Convert.ToInt32(passChar[i]);

                for (int ASCII_Code = 33; ASCII_Code < 48; ASCII_Code++)
                {
                    if(passASCII == ASCII_Code){
                        score++;
                    }
                }

                for (int ASCII_Code = 58; ASCII_Code < 65; ASCII_Code++)
                {
                    if(passASCII == ASCII_Code){
                        score++;
                    }
                }

                for (int ASCII_Code = 91; ASCII_Code < 97; ASCII_Code++)
                {
                    if(passASCII == ASCII_Code){
                        score++;
                    }
                }

                for (int ASCII_Code = 123; ASCII_Code < 127; ASCII_Code++)
                {
                    if(passASCII == ASCII_Code){
                        score++;
                    }
                }
            }

            needsChecking = true;
            break;
        }

        score += passwordLength / 2;

        strengthScore += score;

        if(strengthScore <= 6){
            Debug.Log("Password is too weak, write another one");
        }
        else if(strengthScore >= 7){
            Debug.Log("Not bad");
            Register.gameObject.SetActive(true);
            Enter.gameObject.SetActive(false);
        }
        else if(strengthScore >= 10){
            Register.gameObject.SetActive(true);
            Enter.gameObject.SetActive(false);
            Debug.Log("Very nice");
        }
    }

    public void register(){
        string hashedPassword = sha256(password);
        Debug.Log(hashedPassword);
    }

    public static string sha256(string pass_To_Hash){

        StringBuilder stringB = new StringBuilder();

        var hash = SHA256.Create();

        Encoding encode = Encoding.UTF8; //Used UTF8 because it's hashing a string

        byte[] password = hash.ComputeHash(encode.GetBytes(pass_To_Hash));

        foreach(byte b in password){
            stringB.Append(b.ToString("2x"));
        }

        return stringB.ToString();
    }
}