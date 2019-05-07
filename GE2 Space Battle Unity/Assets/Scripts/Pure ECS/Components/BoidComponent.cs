using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[System.Serializable]
public struct BoidECS : IComponentData
{
    public float mass;
    public Vector3 target;
    public Vector3 force;
    public Vector3 acceleration;
    public Vector3 velocity;
    public float maxSpeed;
    public float maxForce;
}

public class BoidComponent : ComponentDataProxy<BoidECS> { }

