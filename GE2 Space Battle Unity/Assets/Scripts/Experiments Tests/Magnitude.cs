using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnitude : MonoBehaviour
{
    Vector3 myPos;
    public Transform targetTransform;

    private void Awake()
    {
        myPos = transform.position;
    }

    private void Update()
    {
        float dist = GetDistance(myPos, targetTransform.position);
        print(dist);
        
    }


    float GetDistance(Vector3 me, Vector3 target)
    {
        float d = Mathf.Sqrt(Mathf.Pow(target.x - me.x, 2) + Mathf.Pow(target.y - me.y, 2) + Mathf.Pow(target.z - me.z, 2));
        return d;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(myPos, targetTransform.position);
        }
    }

   
}
