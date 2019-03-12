using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float mass;
    public Vector3 velocity= Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 force = Vector3.zero;

    public float maxSpeed;
    public float damping=0.1f;
    public float banking = 0.1f;
    public float maxForce;

    public List<SteeringBehaviour> behaviours;

    private void Start()
    {
        SteeringBehaviour[] bs = GetComponents<SteeringBehaviour>();

        foreach(SteeringBehaviour b in bs)
        {
            //if component is actually active
            if (b.isActiveAndEnabled)
            {
                behaviours.Add(b);
            }
        }
    }

    private void Update()
    {
        force = Calculate();
        Vector3 newAcceleration = force/mass;
        acceleration = Vector3.Lerp(acceleration, newAcceleration, Time.deltaTime);

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        //if velocity is greater than 0
        if (velocity.magnitude > float.Epsilon)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up+(acceleration*banking), Time.deltaTime * 3.0f);

            transform.LookAt(transform.position + velocity, tempUp);
            transform.position += velocity * Time.deltaTime;
            velocity *= (1.0f -(damping * Time.deltaTime));
        }
    }

    //calclulate forces from all components, truncate, prioritise etc..
    Vector3 Calculate()
    {
        force = Vector3.zero;

        foreach (SteeringBehaviour b in behaviours)
        {
            if (b.isActiveAndEnabled)
            {
                force += b.Calculate() * b.weight;

                if (force.magnitude >= maxForce)
                {
                    force = Vector3.ClampMagnitude(force, maxForce);
                    break;
                }
            }
        }
        return force;
    }

    //framework for calculating diff forcess
    public Vector3 SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - velocity;
    }
}
