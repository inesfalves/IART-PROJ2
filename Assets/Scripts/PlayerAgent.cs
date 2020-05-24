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
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles)
        {
            RectTransform rectobj = bubble.GetComponent<RectTransform>();
            AddVectorObs(bubble.GetComponent<BubbleScript>().bubbleValue);
            AddVectorObs(rectobj.anchoredPosition);
        }

    }

    public override void AgentAction(float[] vectorAction)
    {

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
