using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class waypointManagerWindow : EditorWindow{
 
    [MenuItem("Tools/waypoints Editor")]

    public static void Open(){
        GetWindow<waypointManagerWindow>();
    }

    public Transform waypointRoot;

    void OnGUI(){
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if(waypointRoot == null){
            EditorGUILayout.HelpBox("transform not assigned",MessageType.Warning);
        }
        else{
            EditorGUILayout.BeginVertical("Box");
            DrawButtons();
            EditorGUILayout.EndVertical();

        }

        obj.ApplyModifiedProperties();
    }

    void DrawButtons(){
        if(GUILayout.Button("Create waypoint")){
            createWaypoint();
        }


        if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<waypoint>()){
            if(GUILayout.Button("Create Waypoint before")){
                createWaypointBefore();
            }
            
            if(GUILayout.Button("Create Waypoint after")){
                createWaypointAfter();
            }
            if(GUILayout.Button("remove Waypoint")){
                removeWaypoint();
            }

        }
    }

    void createWaypointBefore(){
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount , typeof(waypoint));
        waypointObject.transform.SetParent(waypointRoot , false);
        
        waypoint newWaypoint = waypointObject.GetComponent<waypoint>();

        waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if(selectedWaypoint.previousWaypont != null){
            newWaypoint.previousWaypont = selectedWaypoint.previousWaypont;
            selectedWaypoint.previousWaypont.nextWaypoint = newWaypoint;
        }

        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypont = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void createWaypointAfter(){
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount , typeof(waypoint));
        waypointObject.transform.SetParent(waypointRoot , false);
        
        waypoint newWaypoint = waypointObject.GetComponent<waypoint>();

        waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        newWaypoint.previousWaypont = selectedWaypoint;

        if(selectedWaypoint.nextWaypoint != null){
            selectedWaypoint.nextWaypoint.previousWaypont = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }

        
        
        selectedWaypoint.nextWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void removeWaypoint(){
        waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<waypoint>();
        if(selectedWaypoint.nextWaypoint != null){
            selectedWaypoint.nextWaypoint.previousWaypont = selectedWaypoint.previousWaypont;
        }
        if(selectedWaypoint.previousWaypont != null){
            selectedWaypoint.previousWaypont.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previousWaypont.gameObject;
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }

    void createWaypoint(){
        GameObject waypointObject = new GameObject("waypoint "+ waypointRoot.childCount,typeof(waypoint));
        waypointObject.transform.SetParent(waypointRoot,false);

        waypoint waypoint = waypointObject.GetComponent<waypoint>();
        if(waypointRoot.childCount > 1){
            waypoint.previousWaypont = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<waypoint>();
            waypoint.previousWaypont.nextWaypoint = waypoint;
 
            waypoint.transform.position = waypoint.previousWaypont.transform.position;
            waypoint.transform.forward = waypoint.previousWaypont.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObject;

    }


}
