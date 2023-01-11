using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLook : MonoBehaviour
{
    [Range(0.0f, 500)]
    public float sensitivity;
    public Transform ship;

    public GameObject canvas;

    float xRot = 0;
    float yRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        while(ShipMovement.isActive){
            float xMouse = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float yMouse = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            xRot -= yMouse;
            yRot += xMouse;
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            ship.transform.eulerAngles = new Vector3(xRot, yRot, 0f);

            break;
        }
        if(GameManager.canvOn){
            canvas.SetActive(true);
        }
        else if(!GameManager.canvOn){
            canvas.SetActive(false);
        }
    }
}