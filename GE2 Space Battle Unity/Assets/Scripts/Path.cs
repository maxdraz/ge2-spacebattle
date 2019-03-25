using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> waypoints;

    public int next = 0;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(transform.GetChild(i).transform);
        }
    }

    public int GetNextWaypoint()
    {
        return next;
    }

    public void AdvanceWaypoint()
    {
        next = (next + 1) % waypoints.Count;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;

            for (int i = 0; i < waypoints.Count; i++)
            {
                
                Gizmos.DrawLine(waypoints[i].position, waypoints[(i + 1)%waypoints.Count].position);
            }
        }
    }
}
