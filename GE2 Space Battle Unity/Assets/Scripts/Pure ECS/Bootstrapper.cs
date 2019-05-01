using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//add ECS namespace
using Unity.Entities;


public class Bootstrapper : MonoBehaviour
{
    public struct Move : IComponentData
    {
        public float moveSpeed;
    }
}
