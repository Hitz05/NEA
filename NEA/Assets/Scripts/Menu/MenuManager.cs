using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button start, login, register, quit;

    public void startSim(){
        SceneManager.LoadScene("Simulation");
    }

    public void loginButtn(){
        SceneManager.LoadScene("LogIn");
    }

    public void regButton(){
        SceneManager.LoadScene("Register");
    }

    public void quitSim(){
        Application.Quit();
    }
}
