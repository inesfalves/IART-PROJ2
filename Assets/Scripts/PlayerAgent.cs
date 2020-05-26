using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PlayerAgent : Agent
{

    private GameObject _levelScript;

    // Start is called before the first frame update
    void Start()
    {
        _levelScript = GameObject.Find("LevelScriptEmpty");
    }

    public override void OnEpisodeBegin() { }

    public override void CollectObservations(VectorSensor sensor)
    {
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles)
        {
            Transform rectobj = bubble.GetComponent<Transform>();
            sensor.AddObservation(rectobj.position);
            sensor.AddObservation(bubble.GetComponent<BubbleScript>().bubbleValue);
        }

    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Vector2 controlSignal = Vector2.zero;
        controlSignal.x = -18 + 5*vectorAction[0];
        controlSignal.y = 5 -4*vectorAction[1];

        bool found_bubble = false;

        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles)
        {
            Transform rectobj = bubble.GetComponent<Transform>();
            if(rectobj.position.x == controlSignal.x && rectobj.position.y == controlSignal.y)
            {
                found_bubble = true;
                bubble.GetComponent<BubbleScript>().TouchBubble();
                break;
            }

            if (found_bubble)
            {
                if (found_bubble && _levelScript.GetComponent<LevelScript>().HasWon())
                {
                    SetReward(10.0f);
                    EndEpisode();
                }
                else if (found_bubble && _levelScript.GetComponent<LevelScript>().HasLost())
                {
                    SetReward(-10.0f);
                    EndEpisode();
                }
                else
                {
                    SetReward(-0.5f);
                }
            }
            
            else if(!found_bubble)
            {
                SetReward(-0.7f);
            }
        }

    }

}
