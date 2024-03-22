using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    public Transform playerTarget;
    public Transform mirror;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerLocalPos= mirror.InverseTransformPoint(playerTarget.position);
        transform.position = mirror.TransformPoint(new Vector3(playerLocalPos.x, playerLocalPos.y, - playerLocalPos.z));

        Vector3 lookAtMirror = mirror.TransformPoint(new Vector3(-playerLocalPos.x, playerLocalPos.y, playerLocalPos.z));
        transform.LookAt(lookAtMirror);
    }
}
