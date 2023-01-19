using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class LogIn : MonoBehaviour
{
    Register_LogIn reg; // Imports class Register_Login

    public InputField logUsername, logPassword;
    public Text message;

    public Button createAcc;

    string username, password;

    private void Start() {
        message.color = Color.white;
        message.text = "";

        createAcc.gameObject.SetActive(false);
    }

    string hash(string hashPass){
        byte[] bytes = Encoding.UTF8.GetBytes(hashPass);

        SHA256 hashAlgo = SHA256.Create();
        byte[] hashed = hashAlgo.ComputeHash(bytes);

        string hashedPass = BitConverter.ToString(hashed).Replace("-", "");

        return hashedPass;
    }

    public void createAccount(){
        SceneManager.LoadScene("Register");
    }

    public void logIn(){
        username = logUsername.text;
        password = logPassword.text;

        string fileName = hash(username) + ".txt";
        string filePath = "C:\\pAccounts\\" + fileName;

        if(password == ""){
            message.text = "Password Field cannot be empty";
        }

        while(password != ""){ //Pre-test as it has to run only if the condition is met
            if(File.Exists(filePath)){ // Checks if account exists
            // Debug.Log("You can log in");

                string passToCompare = File.ReadAllText(filePath); //Reads whats in the file then stores it in a variable

                //Hash algorithm will retur the same hashed text if the original is the same
                if(passToCompare == hash(password)){ 
                    Debug.Log("Logged In");
                    SceneManager.LoadScene("Simulation"); // Sends user to simulation once logged in

                }else{
                    message.text = "Wrong Password";
                }

            }else{
                message.color = Color.red;
                message.text = "Account is not recognised, try again or an create account";
                createAcc.gameObject.SetActive(true);
            }

            break;
        }
    }

    public void backToMenu(){
        SceneManager.LoadScene("StartMenu");
    }

    public void quitSim(){
        Application.Quit();
    }
}