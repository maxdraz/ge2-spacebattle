using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[System.Serializable]
public struct SeekECS : IComponentData
{
    public Vector3 target;
    public Vector3 force;
    public float weight;
}


public class SeekComponent : ComponentDataProxy<SeekECS> { }

