using UnityEngine;
using System.Collections.Generic;

public class LocateWaypoints : MonoBehaviour {
    private List<GameObject> Waypoints;

    void Start()
    {
        Waypoints = new List<GameObject>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Waypoint") return;

        Waypoints.Add(other.gameObject);
        Debug.Log("Waypoints = " + Waypoints.Count);

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Waypoint") return;

        Waypoints.Remove(other.gameObject);
        Debug.Log("Waypoints = " + Waypoints.Count);
    }
}
