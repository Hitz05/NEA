using UnityEngine;

public class PredictOrbit : MonoBehaviour
{
    public GameManager gameManager;

    Vector3[] defaultPos; //Stores all the different initial positions of all the planets
    GameObject[] dupePlanetArr;
    GameObject dupePlanet;

    private void Awake() {

        defaultPos = new Vector3[gameManager.body.Length];
        
        for (int i = 0; i < gameManager.body.Length; i++)
        {
            //Get position and store them
            defaultPos[i] = gameManager.body[i].transform.position;
        }
    }

    public void predictOrbit(){
        Debug.Log("Predicting");
        dupePlanetArr = new GameObject[gameManager.body.Length];

        for (int i = 0; i < gameManager.body.Length; i++)
        {   
            //Duplicate every game object and hide those that are not duplicates. Duplicates are given a tag
            //First hide original planets, instantiate duplicates and provide tags
            dupePlanet = Instantiate(gameManager.body[i]);
            dupePlanet.gameObject.tag = "Dupe";

            if(gameManager.body[i].gameObject.tag != "Dupe"){
                gameManager.body[i].gameObject.SetActive(false);
            }

            dupePlanetArr[i] = dupePlanet;
            dupePlanetArr[i].transform.position = defaultPos[i];

            /*To predict orbit:
            1. Make object dissapear
            2. Trail will be seen
            3. Planets move but user will only see the trail
            */

            //1.
            dupePlanetArr[i].GetComponent<MeshRenderer>().enabled = false;
            //2.
            gameManager.trailSwitch = true;
            dupePlanetArr[i].GetComponent<TrailRenderer>().enabled = true;
            dupePlanetArr[i].GetComponent<TrailRenderer>().autodestruct = true;
            dupePlanetArr[i].GetComponent<TrailRenderer>().emitting = true;
            dupePlanetArr[i].GetComponent<TrailRenderer>().time = 20;

            //3.
            dupePlanetArr[i].GetComponent<BodyForce>().enabled = true;
        }
    }
    public void reset(){
        GameObject[] duplicateFind;

        Debug.Log("reset");
        gameManager.trailSwitch = false;

        duplicateFind = new GameObject[GameObject.FindGameObjectsWithTag("Dupe").Length];

        //Find all objects with tag "Dupe"
        duplicateFind = GameObject.FindGameObjectsWithTag("Dupe");

        if(duplicateFind.Length == 0){
            Debug.Log("No duplicates found.");
        }
    }
}