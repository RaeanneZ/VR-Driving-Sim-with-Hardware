using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carNode : Waypoint{

    public carNode link;

    public Vector3 getPosition(){
        Vector3 minBound = transform.position ;
        Vector3 maxBound = transform.position ;
    
        return Vector3.Lerp(minBound, maxBound, Random.Range(0,1));
    }    

}
