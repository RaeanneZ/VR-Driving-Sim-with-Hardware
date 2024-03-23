using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    public float safeDistance = 2f;
    public float carSpeed = 5f;

    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.right, out hit, safeDistance);

        if (hit.transform)
        {
            if (hit.transform.tag == "Car")
            {
                Stop();
            } 
            else
            {
                Move();
            }
        }
    }

    void Stop()
    {
        transform.position += new Vector3(0, 0, 0);
    }

    void Move()
    {
        transform.position += new Vector3(carSpeed * Time.deltaTime, 0, 0);
    }
}
