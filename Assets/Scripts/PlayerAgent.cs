using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PlayerAgent : Agent
{

    private LevelScript _levelScript;

    // Start is called before the first frame update
    void Start()
    {
        _levelScript = GameObject.Find("LevelScriptEmpty").GetComponent<LevelScript>();
    }

    public override void OnEpisodeBegin() {
        _levelScript.ResetTouches();
        _levelScript.ResetBubbles();
     }

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
            controlSignal.x = -18 + 5 * vectorAction[0];
            controlSignal.y = 5 - 4 * vectorAction[1];

            bool found_bubble = false;

            GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
            foreach (GameObject bubble in bubbles)
            {
                Transform rectobj = bubble.GetComponent<Transform>();
                if (rectobj.position.x == controlSignal.x && rectobj.position.y == controlSignal.y)
                {
                    found_bubble = true;
                    bubble.GetComponent<BubbleScript>().TouchBubble();
                    print("que lit");
                    break;
                }
            }

            if (found_bubble)
            {
                if (found_bubble && _levelScript.HasWon())
                {
                    AddReward(1.0f);
                    EndEpisode();
                }
                else if (found_bubble && _levelScript.HasLost())
                {
                    AddReward(-1.0f);
                    EndEpisode();
                }
                else
                {
                    AddReward(-0.05f);
                }
            } else
            {
                AddReward(-0.7f);
            }
        

    }

}
