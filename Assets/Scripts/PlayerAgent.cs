using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PlayerAgent : Agent
{

    private LevelScript _levelScript;
    public List<int> xMask = new List<int>();
    public List<int> yMask = new List<int>();

    public bool found_bubble = false;

    // Start is called before the first frame update
    void Start()
    {
        _levelScript = GameObject.Find("LevelScriptEmpty").GetComponent<LevelScript>();
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");

        foreach (GameObject bubble in bubbles){
            Transform trans = bubble.GetComponent<Transform>();
            switch(trans.position.x){
                case -18:
                    if(!xMask.Contains(0))
                        xMask.Add(0);
                    break;
                case -13:
                    if(!xMask.Contains(1))
                        xMask.Add(1);
                    break;
                case -8:
                    if(!xMask.Contains(2))
                        xMask.Add(2);
                    break;
                case -3:
                    if(!xMask.Contains(3))
                        xMask.Add(3);
                    break;
                case 2:
                    if(!xMask.Contains(4))
                        xMask.Add(4);
                    break;
            }
            switch(trans.position.y){
                case 5:
                    if(!yMask.Contains(0))
                        yMask.Add(0);
                    break;
                case 1:
                    if(!yMask.Contains(1))
                        yMask.Add(1);
                    break;
                case -3:
                    if(!yMask.Contains(2))
                        yMask.Add(2);
                    break;
                case -7:
                    if(!yMask.Contains(3))
                        yMask.Add(3);
                    break;
                case -11:
                    if(!yMask.Contains(4))
                        yMask.Add(4);
                    break;
                case -15:
                    if(!yMask.Contains(5))
                        yMask.Add(5);
                    break;
            }
        }
        for(int i = 0; i < 5; i++){
            if(xMask.Contains(i)){
                xMask.Remove(i);
            }else{
                xMask.Add(i);
            }
        }
        
        for(int i = 0; i < 6; i++){
            if(yMask.Contains(i)){
                yMask.Remove(i);
            }else{
                yMask.Add(i);
            }
        }

        InvokeRepeating("Decide", 3.0f, 3.0f);
    }

    public void Decide(){
        if (GameObject.FindGameObjectsWithTag("TinyBubble").Length == 0){
            if (found_bubble && _levelScript.HasWon()){
                print("won");
                AddReward(1.0f);
                EndEpisode();
            } else if (found_bubble && _levelScript.HasLost()){
                print("lost");
                AddReward(-0.8f);
                EndEpisode();
            }else{
                print("touch");
                AddReward(-0.01f);
            }
            RequestDecision();
        }
    }

    public override void OnEpisodeBegin() {
       // print("Begin");
        _levelScript.ResetTouches();
        _levelScript.ResetBubbles();
    }

    public override void CollectDiscreteActionMasks(DiscreteActionMasker actionMasker){
        actionMasker.SetMask(0, xMask);
        actionMasker.SetMask(1, yMask);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles){
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

            found_bubble = false;
            //print("action");

            GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
            foreach (GameObject bubble in bubbles)
            {
                Transform rectobj = bubble.GetComponent<Transform>();
                if (rectobj.position.x == controlSignal.x && rectobj.position.y == controlSignal.y)
                {
                    found_bubble = true;
                    bubble.GetComponent<BubbleScript>().TouchBubble();
                    break;
                }
            }

            if (!found_bubble){
                print("bad touch");
                AddReward(-0.05f);
            }
        

    }

}
