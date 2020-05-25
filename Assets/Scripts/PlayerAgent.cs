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

    public override void AgentReset()
    {
    }

}
