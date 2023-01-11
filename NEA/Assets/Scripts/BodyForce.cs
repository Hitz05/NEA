using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyForce : MonoBehaviour
{
    [Range(0, 1000)]
    public int initialVel;

    public Vector3 initialVelocity;//Initial orbital velocity of the object
    Vector3 currentVelocity;//Current velocity of the object
    Vector3 F;//Attractive force of planets
    Vector3 forceDir;
    float dist;//Distance between bodies
    float mass;//Mass of body
    public float gravityOfBody;//Gravity of body

    public bool isStar = false; //Checks if object is a star or a celestial body
    bool isFollowing;

    int numOfObjects; //Number of objects in the scene

    Rigidbody rb; //Rigidbody of the object
    TrailRenderer trail;//Trail of the object
    public GameObject centralBody;//Central Body to orbit(mainly for moons)

    GameObject[] body;//Array of all objects in the scene
    public BodyForce[] celestialBody;//Array of all celestial bodies in the scene with this script

    GameObject ship;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        gameObject.tag = "Body";
        initialVelocity = new Vector3(initialVel, 0,0);
    }

    private void Start() {
        getMass(0,0,0,0);
        currentVelocity = initialVelocity;
    }

    private void Update() {
        if(!isStar){
            
            numOfObjects = GameObject.FindGameObjectsWithTag("Body").Length;

            //Set number of objects to be the same as the number of celestial bodies
            if(numOfObjects != celestialBody.Length) {

                body = GameObject.FindGameObjectsWithTag("Body");
                celestialBody = new BodyForce[body.Length];

                for(int i = 0; i < body.Length; i++) {
                    celestialBody[i] = body[i].GetComponent<BodyForce>();
                }
            }
        }

        if(GameManager.enableTrail){
            trail.enabled = true;
            trail.generateLightingData = false;
        }
        else if(!GameManager.enableTrail){
            trail.enabled = false;
            trail.generateLightingData = false;
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);
        Invoke("getForce", 0);
    }

    void getForce(){
        foreach(var otherBody in celestialBody){ //Loops as many times as bodies are in the system
            if(otherBody != this){
                dist = (otherBody.rb.position - rb.position).sqrMagnitude;
                forceDir = (otherBody.rb.position - rb.position).normalized;

                F = forceDir * Constants.G * otherBody.mass * mass / dist;
                currentVelocity += F;
            }
        }
    }

    //Get mass of the object
    void getMass(float radius, float x, float y, float z){
        //Check if all sides are the same length
        x = transform.localScale.x;
        y = transform.localScale.y;
        z = transform.localScale.z;

        if(x == y && y == z && x == z){
            radius = x / 2;
            mass = gravityOfBody * radius / 0.001f;
            rb.mass = mass;
        }
    }
}