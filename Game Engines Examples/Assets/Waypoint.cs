using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Waypoint : MonoBehaviour
{
    private List<Vector3> waypoints = new List<Vector3>();
    private Vector3 target;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.75f;
    public float speed = 0.1f;
    public float rotationspeed = 1f;
    // Start is called before the first frame update
    public int numOfWaypoints = 9;

    public GameObject player;
    private float radius = 10;
    private static StringBuilder message = new StringBuilder();

    public void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "" + message);
        if (Event.current.type == EventType.Repaint)
        {
            message.Length = 0;
        }
    }
    void OnDrawGizmos()
    {
        float theta = Mathf.PI * 2.0f / (float) numOfWaypoints;
        for(int i = 0 ; i < numOfWaypoints ; i ++)
        {
            Vector3 pos = new Vector3(Mathf.Sin(theta * i) * radius, 0, Mathf.Cos(theta * i) * radius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(pos, 1);
            
        }

        // Draw a yellow sphere at the transform's position
       
    }
    void Start()
    {

        float theta = Mathf.PI * 2.0f / (float) numOfWaypoints;
        for(int i = 0 ; i < numOfWaypoints ; i ++)
        {
            //GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Vector3 pos = new Vector3(Mathf.Sin(theta * i) * radius, 0, Mathf.Cos(theta * i) * radius);
            //sp.transform.position = transform.TransformPoint(pos);
            //Debug.Log("Wawypoint:" + sp.transform.position);
            //waypoints.Add(sp.transform);
            waypoints.Add(pos);
        }

        target = waypoints[targetWaypointIndex];

    }

    // Update is called once per frame
    void Update()
    {
        float movementStep = speed * Time.deltaTime;
        float rotationStep = rotationspeed * Time.deltaTime;
        //Vector3 totarget = target.transform.position - transform.position;
        Vector3 totarget = target - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(totarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

        float distanceToTarget = Vector3.Distance(transform.position, target);
        message.Append("Distance to target: " + distanceToTarget + "\n");

        Debug.Log("Distance:" + distanceToTarget);
        transform.position = Vector3.MoveTowards(transform.position, target, movementStep);

        playerTankPosition(transform.position);
        if(distanceToTarget <= minDistance){
            if(targetWaypointIndex > waypoints.Count){
                targetWaypointIndex = 0;
            }
            targetWaypointIndex++;
            UpdateTargetWaypoint();
            
        }
        
    }

    void UpdateTargetWaypoint(){
        target = waypoints[targetWaypointIndex];
    }

    void playerTankPosition(Vector3 currentPos){
        Vector3 totarget = player.transform.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(totarget);
        float detected = Vector3.Dot(totarget, transform.position);

        if(detected > 0){
            message.Append("BLUE TANK IS IN FRONT" + rotationToTarget);
        } else{
            message.Append("BLUE TANK IS IN BEHIND" + rotationToTarget);
        }
    }
}
