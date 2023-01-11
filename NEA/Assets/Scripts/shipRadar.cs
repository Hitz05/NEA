using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipRadar : MonoBehaviour
{
    RaycastHit rHit;
    float distance;

    private void FixedUpdate() {

        //Tells how far a player is fromt he planet
        if(Physics.Raycast(transform.position, transform.forward, out rHit, 100000000)){
            distance = rHit.distance / 100f;
            Debug.Log(rHit.collider.gameObject.name + " is " + Mathf.RoundToInt(distance) + "Km away");
        }
    }
}
