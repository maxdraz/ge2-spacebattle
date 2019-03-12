using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek:SteeringBehaviour
{
    public GameObject targetGO;
    Vector3 target;

    private void Update()
    {
        if (targetGO != null)
        {
            target = targetGO.transform.position;
        }
    }

    public override Vector3 Calculate()
    {
        return boid.SeekForce(target);
    }
}
