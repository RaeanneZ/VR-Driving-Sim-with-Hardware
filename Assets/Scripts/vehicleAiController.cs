using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class vehicleAiController : MonoBehaviour{

    //private CarModifier modifier;
    private WheelCollider[] wheels;
    
    public float totalPower;
    public float vertical , horizontal ;

    private WheelCollider FL_WheelCollider;
    private WheelCollider FR_WheelCollider;
    private WheelCollider BL_WheelCollider;
    private WheelCollider BR_WheelCollider;
    private Transform FL_wheelTransform;
    private Transform FR_wheelTransform;

    private float radius = 8 , distance;
    public carNode currentNode;

    private Vector3 velocity ,Destination, lastPosition;

    void Start(){
        FL_wheelTransform = GameObject.Find("RMCDemo_WheelFrontLeft").transform;
        FR_wheelTransform = GameObject.Find("RMCDemo_WheelFrontRight").transform;

        FL_WheelCollider = GameObject.Find("EU_FL Wheel").GetComponent<WheelCollider>();
        FR_WheelCollider = GameObject.Find("EU_FR Wheel").GetComponent<WheelCollider>();
        BL_WheelCollider = GameObject.Find("EU_BL Wheel").GetComponent<WheelCollider>();
        BR_WheelCollider = GameObject.Find("EU_BR Wheel").GetComponent<WheelCollider>();

        wheels = new WheelCollider[4] { FL_WheelCollider, FR_WheelCollider, BL_WheelCollider, BR_WheelCollider };

    }

    void FixedUpdate(){
        try{
        checkDistance();
        steerVehicle();
        }
        catch{}
    
    }


    void checkDistance(){

            if(Vector3.Distance(transform.position , currentNode.transform.position) <= 3){
                reachedDestination();
            }

        
    }

    // When car reaches the node, check for the next node
    // If no more next node (means end of route) then do nothing, else target the next node
    private void reachedDestination(){
        if(currentNode.nextWaypoint == null ){
            currentNode = (carNode)currentNode.previousWaypoint;
            return;
        }
        if(currentNode.previousWaypoint == null ){
            currentNode = (carNode)currentNode.nextWaypoint;
            return;
        }

        // Randomly decides which route to take if there's more than 1 path
        if(currentNode.link != null && Random.Range(0 , 100) <= 20)
            currentNode = currentNode.link;
        else
            currentNode = (carNode)currentNode.nextWaypoint;

    }

    // This is to handle car movement
    private void steerVehicle()
    {
        // Calculating steering angle needed
        Vector3 relativeVector = transform.InverseTransformPoint(currentNode.transform.position);
        relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * 2;
        horizontal = newSteer;

        // 4 wheel drive - apply the motor to all wheel colliders
        foreach (WheelCollider item in wheels)
        {
            item.motorTorque = totalPower;
        }

        // if car moving to the right 
        if (horizontal > 0)
        {
            FL_WheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            FR_WheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
        }
        else if (horizontal < 0) // if car moving to the left
        {
            FL_WheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
            FR_WheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
        }
        else
        {
            FL_WheelCollider.steerAngle = 0;
            FR_WheelCollider.steerAngle = 0;
        }

    }

    // Visual reference in Unity for debugging where the current node is, not seen in the game view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(currentNode != null)
        Gizmos.DrawSphere(currentNode.transform.position ,0.5f);
    }

}
