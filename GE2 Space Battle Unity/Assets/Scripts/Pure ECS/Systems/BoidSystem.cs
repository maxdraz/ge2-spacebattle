using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst; // to use burst compiler
using Unity.Transforms;

public class BoidSystem : JobComponentSystem
{
    #region JOB HANDLE
    // On Update
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //Boid 
        var boidJob = new BoidJob()
        {
            dT = Time.deltaTime
        };
        var boidHandle = boidJob.Schedule(this, inputDeps);

        //Seek
        var seekJob = new SeekJob()
        {
            dT = Time.deltaTime
        };

        var seekHandle = seekJob.Schedule(this, boidHandle);
        
        //Move
        var moveJob = new MoveJob()
        {
            dT = Time.deltaTime
        };

        return  moveJob.Schedule(this, seekHandle);

       
    }
    #endregion

    #region JOBS    
    [BurstCompile]
    public struct BoidJob : IJobProcessComponentData<BoidECS, Position, SeekECS>
    {
        public float dT;

        //function to accumulate all forces
        public Vector3 AccumulateForces(ref BoidECS b, ref SeekECS s)
        {
            Vector3 force = Vector3.zero;

            force += s.force;
            if(force.magnitude >= b.maxForce)
            {
                force = Vector3.ClampMagnitude(force, b.maxForce);
                return force;
            }

            return force;
        }

        public void Execute(ref BoidECS b, ref Position p, ref SeekECS s)
        {
            //translate force to acceleration and add it to speed each frame
            b.force = AccumulateForces(ref b, ref s);
            b.acceleration = b.force / b.mass;
            b.velocity += b.acceleration * dT;

            //clamp speed
            b.velocity = Vector3.ClampMagnitude(b.velocity, b.maxSpeed);
            float speed = b.velocity.magnitude;
            //if speed is greater than 0
            if(speed > 0)
            {
                Vector3 pos = p.Value;
                pos += b.velocity * dT;

                p.Value = pos;
            }
            
        }
    }

    [BurstCompile]
    public struct MoveJob : IJobProcessComponentData<BoidECS,Position, Move>
    {
        public float dT;

        public void Execute(ref BoidECS b ,ref Position p, ref Move m)
        {
            //ref target and pos
            Vector3 pos = p.Value;
            Vector3 target = b.target;
            //find toTarget vector
            Vector3 toTarget = target - pos;
            toTarget.Normalize();
            // add speed
            pos += toTarget * m.speed * dT;
            //make the original Position value pos
            p.Value = pos;
        }
    }
    [BurstCompile]
    public struct SeekJob : IJobProcessComponentData<BoidECS,Position, SeekECS>
    {
        public float dT;

        public void Execute(ref BoidECS b, ref Position p, ref SeekECS s)
        {
            Vector3 target = s.target;
            Vector3 pos = p.Value;
            Vector3 toTarget = (s.target - pos).normalized;

            s.force = b.velocity - toTarget;

        }
    }
    #endregion
}
