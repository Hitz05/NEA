using UnityEngine;

public class PredictOrbit : MonoBehaviour
{

    public GameManager gameManager;

    GameObject dupe; //Stores the duplicate planets
    GameObject[] dupeArr;//Stores all duplicates on an array so that their position can be reset

    Vector3[] defaultLoc;

    private void Awake() {

        defaultLoc = new Vector3[gameManager.body.Length];
        dupeArr = new GameObject[gameManager.body.Length];

        for (int i = 0; i < gameManager.body.Length; i++)
        {
            defaultLoc[i] = gameManager.body[i].transform.position;
        }
    }

    private void Update() {
        if(!gameManager.predicting){ //It will destroy every duplicate if its not predicting the orbit
            for (int j = 0; j < gameManager.body.Length; j++)
            {
                Destroy(dupeArr[j]);
            }
        }
    }

    public void predict(){
        gameManager.predicting = !gameManager.predicting;

        Debug.Log(gameManager.predicting);

        for (int i = 0; i < gameManager.body.Length; i++) //This will loop through every planet
        {
            gameManager.body[i].transform.position = defaultLoc[i]; //It will first reset the location of every object

            dupe = Instantiate(gameManager.body[i]); //It will duplicate those planets
            dupe.gameObject.tag = "Dupe";//Adds tag "Dupe"(short for duplicate)

            dupeArr[i] = dupe; //Adds duplicates to array

            dupeArr[i].transform.position = defaultLoc[i]; //It will set their location to the start

            if(gameManager.body[i].gameObject.tag != "Dupe"){ //If the planet is not a duplicate, it will be invisible until duplicate is removed
                dupe.GetComponent<BodyForce>().enabled = true;
                gameManager.body[i].GetComponent<BodyForce>().enabled = false;
                gameManager.body[i].gameObject.SetActive(false);
            }
            else{
                dupe.GetComponent<BodyForce>().enabled = false;
                gameManager.body[i].GetComponent<BodyForce>().enabled = true;
                gameManager.body[i].gameObject.SetActive(true);
            }
        }

        if(!gameManager.predicting){
            for (int i = 0; i < gameManager.body.Length; i++)
            {
                dupeArr[i].GetComponent<BodyForce>().enabled = false;
                dupeArr[i].gameObject.SetActive(false);
            }
        }
    }
}