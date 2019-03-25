using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : SteeringBehaviour
{
    public Path path;

    private void Start()
    {
        if (path == null)
        {
            print("no path specified");
        }
    }

    public override Vector3 Calculate()
    {
        if (Vector3.Distance(path.waypoints[path.next].position, transform.position) < 1f)
        {
            path.AdvanceWaypoint();
        }
        
       return boid.SeekForce(path.waypoints[path.GetNextWaypoint()].position);
    }
}
