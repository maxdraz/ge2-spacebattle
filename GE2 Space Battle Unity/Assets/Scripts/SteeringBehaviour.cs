using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public abstract class SteeringBehaviour : MonoBehaviour
{
    public float weight = 1f;
    [HideInInspector]
    public Boid boid;

    private void Awake()
    {
        boid = GetComponent<Boid>();
    }

    public abstract Vector3 Calculate();
}
