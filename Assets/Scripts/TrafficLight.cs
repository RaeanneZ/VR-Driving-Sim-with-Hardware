using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class TrafficLight : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int[] relatedID;
    [SerializeField] private GameObject[] trafficLightCirclesArray;
    [SerializeField] private Material[] trafficColouredMaterialsArray;
    [SerializeField] private Material trafficUncolouredMaterial;
    [SerializeField] private int currentTrafficLight;
    private Dictionary<GameObject, Material> trafficLightDict = new Dictionary<GameObject, Material>();

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public int[] RelatedID
    {
        get { return relatedID; }
        set { relatedID = value; }
    }

    public GameObject[] TrafficLightCirclesArray
    {
        get { return trafficLightCirclesArray; }
        set { trafficLightCirclesArray = value; }
    }

    public Material[] TrafficColouredMaterialsArray
    {
        get { return trafficColouredMaterialsArray; }
        set { trafficColouredMaterialsArray = value; }
    }

    public int CurrentTrafficLight
    {
        get { return currentTrafficLight; }
        set { currentTrafficLight = value; }
    }

    public TrafficLight(GameObject[] trafficLightCirclesArray, Material[] trafficColouredMaterialsArray, Material trafficUncolouredMaterial)
    {
        // Constructor
        currentTrafficLight = 2;
    }
    public TrafficLight(GameObject[] trafficLightCirclesArray, Material[] trafficColouredMaterialsArray, Material trafficUncolouredMaterial, int currentTrafficLight)
    {
        // Constructor
    }

    public void ColourTrafficLight(GameObject trafficLightCircle)
    {
        int index = Array.IndexOf(trafficLightCirclesArray, trafficLightCircle);
        trafficLightCircle.GetComponent<Renderer>().material = TrafficColouredMaterialsArray[index];
    }

    // Grey out inactive traffic light
    public void GreyTrafficLight(GameObject trafficLightCircle)
    {
        trafficLightCircle.GetComponent<Renderer>().material = trafficUncolouredMaterial;
    }
}
