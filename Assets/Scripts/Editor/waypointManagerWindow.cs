using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class waypointManagerWindow : EditorWindow {
 
    [MenuItem("Tools/waypoints Editor")]

    public static void Open(){
        GetWindow<waypointManagerWindow>();
    }

    // Parent container that contains all the waypoints for that route
    public Transform waypointRoot;

    /// <summary>
    /// Draws the UI in editor for easier reference
    /// </summary>
    void OnGUI(){
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if(waypointRoot == null){
            EditorGUILayout.HelpBox("Please select a root transform",MessageType.Warning);
        }
        else{
            EditorGUILayout.BeginVertical("Box");
            DrawButtons();
            EditorGUILayout.EndVertical();

        }

        obj.ApplyModifiedProperties();
    }

    /// <summary>
    /// UI to draw buttons on possible lists of actions
    /// </summary>
    void DrawButtons(){
        if(GUILayout.Button("Create waypoint")){
            createWaypoint();
        }


        if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>()){
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

    void createWaypoint()
    {
        GameObject waypointObject = new GameObject("waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if (waypointRoot.childCount > 1)
        {
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;

            Debug.Log("Next Waypoint: " + waypoint.name);

            // Place the waypoint at the last position
            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObject;

    }

    void createWaypointBefore(){
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount , typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot , false);
        
        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if(selectedWaypoint.previousWaypoint != null){
            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.nextWaypoint = newWaypoint;
        }

        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void createWaypointAfter(){
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount , typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot , false);
        
        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        newWaypoint.previousWaypoint = selectedWaypoint;

        if(selectedWaypoint.nextWaypoint != null){
            selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }

        
        
        selectedWaypoint.nextWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void removeWaypoint(){
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        if(selectedWaypoint.nextWaypoint != null){
            selectedWaypoint.nextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
        }
        if(selectedWaypoint.previousWaypoint != null){
            selectedWaypoint.previousWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject;
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }

    public static void OnDrawGizmos(carNode waypoint, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0)
            Gizmos.color = Color.white;
        else
            Gizmos.color = Color.white;


        Gizmos.DrawSphere(waypoint.transform.position, 0.1f);
        if (waypoint.nextWaypoint != null && waypoint.previousWaypoint != null)
        {
            Gizmos.DrawLine(waypoint.transform.position, waypoint.previousWaypoint.transform.position);

        }
        else if (waypoint.previousWaypoint == null && waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(waypoint.transform.position, waypoint.nextWaypoint.transform.position);
        }
        else
        {
            Gizmos.color = Color.yellow;
            if (waypoint.previousWaypoint != null)
                Gizmos.DrawLine(waypoint.transform.position, waypoint.previousWaypoint.transform.position);
        }

        if (waypoint.link != null)
            Gizmos.DrawLine(waypoint.transform.position, waypoint.link.transform.position);
    }
}
