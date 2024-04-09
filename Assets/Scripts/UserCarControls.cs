using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class UserCarControls : MonoBehaviour
{
    public InputActionMap inputActionMap;
    
    private WheelCollider FL_WheelCollider;
    private WheelCollider FR_WheelCollider;
    private WheelCollider BL_WheelCollider;
    private WheelCollider BR_WheelCollider;
    private Transform FL_wheelTransform;
    private Transform FR_wheelTransform;
    private Transform offset;
    private Transform steeringGameObject;
    private TextMeshProUGUI alertText;
    private TextMeshProUGUI speedText;

    private float gasPedalValue;
    private float brakePedalValue; 
    private float switchReverseModeValue;
    private float switchDrivingModeValue;
    private string currentMode = "D";
    private float defaultAcceleration = 10f;
    private float accelerationForce = 1500f;
    private float brakeForce = 2000f;
    private float maxSteeringAngle = 30;

    // Start is called before the first frame update
    void Start()
    {
        // Get the elements needed
        FL_WheelCollider = GameObject.Find("FL Wheel").GetComponent<WheelCollider>();
        FR_WheelCollider = GameObject.Find("FR Wheel").GetComponent<WheelCollider>();
        BL_WheelCollider = GameObject.Find("BL Wheel").GetComponent<WheelCollider>();
        BR_WheelCollider = GameObject.Find("BR Wheel").GetComponent<WheelCollider>();

        FL_wheelTransform = GameObject.Find("RMCDemo_WheelFrontLeft").transform;
        FR_wheelTransform = GameObject.Find("RMCDemo_WheelFrontRight").transform;

        offset = GameObject.Find("Offset Target").transform;
        steeringGameObject = GameObject.Find("Steering Object").transform;

        alertText = GameObject.Find("Gear Shift Alert").GetComponent<TextMeshProUGUI>();
        alertText.text = currentMode;

        speedText = GameObject.Find("Speed UI").GetComponent<TextMeshProUGUI>();

        // This is to get the player inputs to work (DO NOT DELETE)
        inputActionMap.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        JoystickControl();
        PedalControl();
        SteeringWheelControl();
    }

    /// <summary>
    /// This is for steering wheel, control car to move left or right, turn etc
    /// </summary>
    void SteeringWheelControl()
    {
        Vector2 steeringWheelInputValue = inputActionMap.FindAction("Steer").ReadValue<Vector2>();

        // Get the offset vector according to the steering wheel input (x,z); y = 0
        float offsetX = steeringWheelInputValue.x;
        float offsetZ = Mathf.Sqrt(1 - (offsetX * offsetX)); // Using Pythagoras Theorem, the formula from input steering wheel: y^2 = 1 - x^2

        offset.localPosition = new Vector3(offsetX, 0, offsetZ);

        // Rotating steering wheel about y axis
        float steeringAngle = offsetX * maxSteeringAngle;
        float steeringAngleInDeg = offsetX * Mathf.Rad2Deg;
        steeringGameObject.localEulerAngles = new Vector3(0, steeringAngleInDeg, 0);

        // Show the wheel transformation change
        FL_wheelTransform.localEulerAngles = new Vector3(0, steeringAngle, 0);
        FR_wheelTransform.localEulerAngles = new Vector3(0, steeringAngle, 0);

        // Change steering angle to move car in the direction 
        FL_WheelCollider.steerAngle = steeringAngle;
        FR_WheelCollider.steerAngle = steeringAngle;       
    }

    /// <summary>
    /// This is for the joystick controls, switch between reverse mode and driving mode
    /// </summary>
    void JoystickControl()
    {
        // Get value for Joystick
        switchReverseModeValue = inputActionMap.FindAction("Reverse Mode").ReadValue<float>();
        switchDrivingModeValue = inputActionMap.FindAction("Driving Mode").ReadValue<float>();

        // Change mode only when player explicitly makes the change
        if (switchReverseModeValue == 1 && currentMode != "R")
        {
            currentMode = "R";
            alertText.text = "R";

            defaultAcceleration *= -1;
            accelerationForce *= -1;
        }
        else if (switchDrivingModeValue == 1 && currentMode != "D")
        {
            currentMode = "D";
            alertText.text = "D";

            defaultAcceleration *= -1;
            accelerationForce *= -1;
        }
    }

    /// <summary>
    /// This is for the pedal controls, controls car to accelerate and brake
    /// </summary>
    void PedalControl()
    {
        // Get the value of the left and right pedals, values are 1 (rest state) ... -1 (pedals are floored)
        float gasPedalInput = inputActionMap.FindAction("Accelerate").ReadValue<float>(); 
        float brakePedalInput = inputActionMap.FindAction("Brake").ReadValue<float>();

        // This is for calculation later to display in UI
        float totalAccF = 0;
        float totalBrakeF = 0;

        // Change the value from 1 ... -1 to 0 ... 1
        gasPedalValue = ConvertInputValue(gasPedalInput);
        brakePedalValue = ConvertInputValue(brakePedalInput);

        // Check if any pedals are stepped on, use their values, else go by default
        if (gasPedalValue > 0 || brakePedalValue > 0)
        {
            totalAccF = Accelerate(gasPedalValue);
            totalBrakeF = Brake(brakePedalValue);
        }
        else
        {
            // By Default
            FL_WheelCollider.brakeTorque = 0;
            FR_WheelCollider.brakeTorque = 0;
            BL_WheelCollider.brakeTorque = 0;
            BR_WheelCollider.brakeTorque = 0;

            BL_WheelCollider.motorTorque = defaultAcceleration;
            BR_WheelCollider.motorTorque = defaultAcceleration;

            totalAccF = defaultAcceleration;
            totalBrakeF = 0;
        }

        // For UI
        speedText.text = Mathf.FloorToInt(gameObject.GetComponent<Rigidbody>().velocity.magnitude * 5).ToString() + " km/h";
    } 

    /// <summary>
    /// Change the value from 1 ... -1 to 0 ... 1
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    float ConvertInputValue(float value)
    {
        if (value == 1)
        {
            return 0;
        }
        else if (value == 0)
        {
            return 0.5f;
        }
        else if (value == -1)
        {
            return 1;
        }
        else if (value > 0 && value < 1)
        {
            return (1 - value)/10;
        } 
        else
        {
            return Mathf.Abs(value) + 0.05f;
        }
    }

    public float Accelerate(float value)
    {
        float totalAccF = defaultAcceleration + accelerationForce * value;

        BL_WheelCollider.motorTorque = totalAccF;
        BR_WheelCollider.motorTorque = totalAccF;

        return totalAccF;
    }

    public float Brake(float value)
    {
        float totalBrakeF = brakeForce * value;

        FL_WheelCollider.brakeTorque = totalBrakeF;
        FR_WheelCollider.brakeTorque = totalBrakeF;
        BL_WheelCollider.brakeTorque = totalBrakeF;
        BR_WheelCollider.brakeTorque = totalBrakeF;

        return totalBrakeF;
    }
}
