using UnityEngine;

public class PredictOrbit : MonoBehaviour
{

    public GameManager gameManager;

    GameObject dupe; //Stores the duplicate planets

    public void predict(){
        gameManager.predicting = !gameManager.predicting;

        for (int i = 0; i < gameManager.body.Length; i++)
        {
            dupe = Instantiate(gameManager.body[i]);
            dupe.gameObject.tag = "Dupe";

            if(gameManager.body[i].gameObject.tag != "Dupe"){
                dupe.GetComponent<BodyForce>().enabled = true;
                gameManager.body[i].GetComponent<BodyForce>().enabled = false;
                gameManager.body[i].gameObject.SetActive(false);
            }
        }

        if(!gameManager.predicting){
            Destroy(dupe);
        }
    }
}