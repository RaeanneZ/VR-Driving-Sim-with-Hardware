using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModifier : MonoBehaviour
{
    internal enum type
    {
        player,
        AI,
    }
    [SerializeField] private type playerType;

    [Header("wheels")]
    [Range(0.2f, 0.7f)] public float wheelRadius = 0.36f;
    [Range(0.05f, 0.2f)] public float suspensionDistance = 1.0f;
    [Range(0, 0.1f)] public float suspensionOffset = 0.03f;
    [Range(0.4f, 1)] public float sidewaysFriction;
    [Range(0.5f, 1)] public float forwardFriction;

    private CarController controller;
    private GameObject wheelsFolder;
    private GameObject[] wheels;
    private Vector3 wheelPosition;
    private Quaternion wheelRotation;
    [HideInInspector] public WheelCollider[] colliders;

    private void Awake()
    {
        controller = GetComponent<CarController>();
        wheelsFolder = gameObject.transform.Find("wheels").gameObject;

        wheels = new GameObject[wheelsFolder.transform.childCount - 1];
        colliders = new WheelCollider[wheels.Length];

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i] = wheelsFolder.transform.GetChild(i + 1).gameObject;
        }

        GameObject wheelObject = wheelsFolder.transform.GetChild(0).gameObject;
        spawnWheelColliders();
        spawn
    }
}
