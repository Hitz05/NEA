using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float currentSpeed = 0;
    float maxSpeed = 400f;

    public static bool isActive; //Check if user is lookign through player camera

    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        while(isActive){
            if(currentSpeed <= maxSpeed){
                if(Input.GetKey(KeyCode.LeftShift)){
                    currentSpeed += 0.4f;
                    if(currentSpeed > maxSpeed){
                        currentSpeed = maxSpeed;
                    }
                }
                else if(Input.GetKey(KeyCode.LeftControl)){
                    currentSpeed -= 0.4f;

                    if(currentSpeed < 0){
                        currentSpeed = 0;
                    }
                }
            }

            transform.position -= transform.forward * -currentSpeed * Time.deltaTime;
            break;
        } 
    }
}
