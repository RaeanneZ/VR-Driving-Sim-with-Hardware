using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    TrafficLight[] trafficLightArray;
    public float trafficLightTimeInterval = 10f;
    public float trafficLightTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        trafficLightArray = GameObject.FindObjectsOfType<TrafficLight>();

        // Reset traffic lights to grey and colour last circle
        foreach (TrafficLight trafficLight in trafficLightArray)
        {
            foreach(GameObject trafficCircle in trafficLight.TrafficLightCirclesArray)
            {
                trafficLight.GreyTrafficLight(trafficCircle);
            }
            
            trafficLight.ColourTrafficLight(trafficLight.TrafficLightCirclesArray[trafficLight.CurrentTrafficLight]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Timer to track timing
        trafficLightTimer += Time.deltaTime;

        // When the timer hits the traffic light interval timing 
        if (trafficLightTimer >= trafficLightTimeInterval)
        {
            // Reset traffic lights to grey and colour last circle
            foreach (TrafficLight trafficLight in trafficLightArray)
            {
                //If it is red, next colour is green
                if (trafficLight.CurrentTrafficLight == 0)
                {
                    trafficLight.CurrentTrafficLight = 2;
                }
                else
                {
                    trafficLight.CurrentTrafficLight -= 1;
                }

                foreach (GameObject trafficCircle in trafficLight.TrafficLightCirclesArray)
                {
                    trafficLight.GreyTrafficLight(trafficCircle);
                }

                trafficLight.ColourTrafficLight(trafficLight.TrafficLightCirclesArray[trafficLight.CurrentTrafficLight]);
            }

            // Reset traffic light timer
            trafficLightTimer = 0;
        }

    }
}
