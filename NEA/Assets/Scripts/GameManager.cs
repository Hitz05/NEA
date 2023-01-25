using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeFlow = 1f;

    int camCount;//Number of cameras in the scene
    public GameObject[] cameras;//Array of all cameras in the scene

    int index = 0;//Keeps track which object in cameras array is being selected

    public static bool enableTrail = true; //Enables trail of planets(Bad Performance)
    public bool trailSwitch = true;//Enables trail of planets(Bad Performance)

    public static bool canvOn;
    bool isPlaying = false;

    public GameObject[] body;

    public Button predict;
    public Button play;

    public bool predicting;

    void Awake(){
        timeFlow = 1;

        body = GameObject.FindGameObjectsWithTag("Body");

        Debug.Log(body.Length);
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeFlow; //Keeps time of simulation

        if(!isPlaying){ // Will disable physics if the start button is pressed so that the user can change things
            for (int i = 0; i < body.Length; i++)
            {
                body[i].GetComponent<BodyForce>().enabled = false;
            }

            predict.gameObject.SetActive(true);

        }else{
            for (int i = 0; i < body.Length; i++)
            {
                body[i].GetComponent<BodyForce>().enabled = true;
            }

            predict.gameObject.SetActive(false);
        }

        camSwap(0, 0);

        if(trailSwitch){
            enableTrail = true;
        }
        else{
            enableTrail = false;
        }

        isPredicting();
    }

    void camSwap(int arrLimiter, int n){
        camCount = Camera.allCameras.Length; //Get how many cameras there are in the scene
        cameras = GameObject.FindGameObjectsWithTag("Camera");//Get all cameras in the scene

        if (camCount >= 1){ //Only works if there's more than one camera
            arrLimiter = camCount - 1;
            
            //Changes the camera
            if (Input.GetKeyDown(KeyCode.RightBracket)){ //Keyboard Input
                index++;
                if (index > arrLimiter){
                    index = arrLimiter;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftBracket)){ //Keyboard Input
                index--;
                if(index < 0){
                    index = 0;
                }
            }

            while (camCount >= 1){//Only works if theres more than 1 camera in the scene
                while (index != n){//Allows to disable every other camera
                    for (n = 0; n < cameras.Length; n++) //Disables every camera
                    {
                        cameras[n].GetComponent<Camera>().enabled = false;
                    }

                    break;
                }
                cameras[index].GetComponent<Camera>().enabled = true;

                if (cameras[index].name == "Ship Cam"){ //Allows ship to move
                    ShipMovement.isActive = true;
                    canvOn = true;
                }
                else {
                    ShipMovement.isActive = false;
                    canvOn = false;
                }

                break;
            }
        }
    }

    public void MainMenu(){
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitSim(){
        Application.Quit();
    }

    public void Start_Pause(){
        isPlaying = !isPlaying;

        Debug.Log(isPlaying);
    }

    void isPredicting(){
        if(predicting){
            play.gameObject.SetActive(false);
        }else{
            play.gameObject.SetActive(true);
        }
    }
}