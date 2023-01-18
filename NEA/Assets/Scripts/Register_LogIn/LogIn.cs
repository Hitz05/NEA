using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{

    Register_LogIn reg; // Imports class Register_Login

    public InputField logUsername, logPassword;

    public string username, password;

    public void logIn(){
        username = logUsername.text;
        password = logPassword.text;

        Debug.Log(reg.hash(username));
        Debug.Log(reg.hash(password));
    }
}