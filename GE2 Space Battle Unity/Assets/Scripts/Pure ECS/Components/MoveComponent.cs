using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[System.Serializable]
public struct Move: IComponentData
{    
    public float speed;
}

public class MoveComponent : ComponentDataProxy<Move> { }

