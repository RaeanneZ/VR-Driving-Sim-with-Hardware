using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class carModifier : MonoBehaviour
{
   internal enum type
    {
        player;
        AI;
    }

    [SerializeField] private type playerType;

    [Header("wheels")]
    [Range(0.2f, 0.7f)] public float wheelRadius = 0.36f;
    [Range(0.05f, 0.2f)] public float suspensionDistance = 0.1f;
    [Range(0, 0.1f)] public float suspensionOffset = 0.03f;
    [Range(0.4f, 1)] public float sidewaysFriction;
    [Range(0.5f, 1)] public float forwardFriction;

    private carController controller;
    private GameObject wheelsFolder;
    private GameObject[] wheels;
    private Vector3 wheelPosition;
    private Quarternion wheelRotation;
    [HideInInspector] public WheelCollider[] colliders;

    void Awake()
    {
        controller = gameObject.GetComponent<carController>();
        wheelsFolder = gameObject.transform.Find("wheels").gameObject;

        wheels = new GameObject[wheelsFolder.transform.childCount - 1];
        colliders = new WheelCollider[wheels.Length];

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i] = wheelsFolder.transform.GetChild(0).gameobject;
        }

        GameObject wheelObject = wheelsFolder.transform.GetChild(0).gameObject;

        spawnWheelColliders();
        spawnWheels(wheelObject);
    }

    void FixedUpdate()
    {
        animateWheels();
    }

    public void spawnWheels(GameObject wheel)
    {

    }
)