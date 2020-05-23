using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PlayerAgent : Agent
{
    // Start is called before the first frame update
    void Start()
    { 
    }

    public override void InitializeAgent() { }

    public override void CollectObservations()
    {
        //target n agent pos
        AddVectorObs(Target.position);
        AddVectorObs(transform.position);

        //agent velocity
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }

    public float speed = 10;

    public override void AgentAction(float[] vectorAction)
    {
        //Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];

        rBody.AddForce(controlSignal * speed);

        //Rewards
        float distanceToTarget = Vector3.Distance(transform.position, Target.position);

        //Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            Done();
        }

        //Fell off platform
        if (transform.position.y < 0)
            Done();

    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }

    public override void AgentReset()
    {
        if(transform.position.y <0)
        {
            //if agent fell, zero its momentum
            rBody.angularVelocity = Vector3.zero;
            rBody.velocity = Vector3.zero;
            transform.position = new Vector3(0, 0.5f, 0);
        }

        //move the target to a new spot
        Target.position = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

}
