using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PlayerAgent : Agent
{

    private GameObject _levelScript;

    // Start is called before the first frame update
    void Start()
    {
        _levelScript = GameObject.Find("LevelScriptEmpty");
    }

    public override void InitializeAgent() { }

    public override void CollectObservations()
    {
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles)
        {
            RectTransform rectobj = bubble.GetComponent<RectTransform>();
            AddVectorObs(rectobj.anchoredPosition);
            AddVectorObs(bubble.GetComponent<BubbleScript>().bubbleValue);
        }

    }

    public override void AgentAction(float[] vectorAction)
    {
        Vector2 controlSignal = Vector2.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.y = vectorAction[1];

        bool found_bubble = false;

        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles)
        {
            RectTransform rectobj = bubble.GetComponent<RectTransform>();
            if(rectobj.anchoredPosition.x == controlSignal.x && rectobj.anchoredPosition.y == controlSignal.y)
            {
                found_bubble = true;
                bubble.GetComponent<BubbleScript>().TouchBubble();
                break;
            }

            if (found_bubble)
            {
                if (found_bubble && _levelScript.GetComponent<LevelScript>().HasWon())
                {
                    SetReward(1.0f);
                    Done();
                }
                else if (found_bubble && _levelScript.GetComponent<LevelScript>().HasLost())
                {
                    SetReward(-1.0f);
                    Done();
                }
                else
                {
                    SetReward(-0.2f);
                }
            }
            
            else if(!found_bubble)
            {
                SetReward(-0.7f);
            }
        }

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
