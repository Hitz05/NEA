using UnityEngine;

public class PredictOrbit : MonoBehaviour
{

    public GameManager gameManager;

    GameObject dupe;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameManager.body.Length; i++)
        {
            dupe = Instantiate(gameManager.body[i]);

            dupe.GetComponent<SphereCollider>().enabled = false;
            // dupe.GetComponent<BodyForce>().enabled = false;
        }
    }
}
