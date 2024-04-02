using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    // Clean up scene by deleting everything once user stops game
    private void OnDestroy()
    {
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            Destroy(obj);
            Debug.Log(obj + "is destroyed!");
        }
    }
}
