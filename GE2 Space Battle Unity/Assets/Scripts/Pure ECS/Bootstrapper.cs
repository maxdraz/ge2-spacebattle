using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random; // do this to avoid conflicts between math ran
using Unity.Collections;
//add ECS namespaces
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class Bootstrapper : MonoBehaviour
{
    
    private EntityManager manager;

    [Header("Entities")]
    public Transform targetTrans;
    public List<EntityToSpawn> entitiesToSpawn;

    //public int entityCount;
    //public GameObject entityTemplate;
    
    

    private void Start()
    {
        //REF ENTITY MANAGER
        manager = World.Active.GetOrCreateManager<EntityManager>();
        //INSTANTIATE ENTITIES        
        for (int i = 0; i < entitiesToSpawn.Count; i++)
        {
            EntityToSpawn thisE = entitiesToSpawn[i];

            SpawnShips(thisE.entityTemplate, thisE.amountToSpawn);
        }

    }

    #region SPAWNING
    void SpawnShips(GameObject template, int count)
    {
        //make array to temp store Entities
        var entities = new NativeArray<Entity>(count, Allocator.Temp);

        

        for (int i = 0; i < count-1; i++)
        {
            //get random pos
            float ranX = Random.Range(1f, 50f);
            float ranZ = Random.Range(1f, 50f);
            //instantiate
            manager.Instantiate(template,entities);
            //set pos and rotation
            float3 pos = new float3(ranX, 0, ranZ);
            manager.SetComponentData(entities[i], new Position { Value = pos });
            
            quaternion rot = new quaternion(-1,0,0,1);            
            
            manager.SetComponentData(entities[i], new Rotation {Value = rot});
           // manager.SetComponentData(entities[i], new BoidECS { target = targetTrans.position });
            manager.SetComponentData(entities[i], new SeekECS { target = targetTrans.position });

        }

        Debug.Log(entities.Length);

        entities.Dispose();
    }
    #endregion

}

[Serializable]
public class EntityToSpawn {
    public string name;
    public GameObject entityTemplate;
    public int amountToSpawn;
}

