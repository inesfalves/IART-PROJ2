using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject tinyBubble;
    public int bubbleValue;
    private GameObject _levelScript;


    // Start is called before the first frame update
    void Start()
    {
        _levelScript = GameObject.Find("LevelScriptEmpty");
    }

    // Update is called once per frame
    void Update()
    {
        if(bubbleValue > 0)
            this.GetComponent<SpriteRenderer>().sprite = sprites[bubbleValue-1]; 
    }

    void OnMouseDown(){
        if (GameObject.FindGameObjectsWithTag("TinyBubble") == null || GameObject.FindGameObjectsWithTag("TinyBubble").Length == 0)
            TouchBubble();
    }  

    public void TouchBubble()
    {
        bubbleValue--;
        if (bubbleValue == 0)
        {
            BurstBubbleTest();
        }

        _levelScript.GetComponent<LevelScript>().UpdateTouches();
    }

    void BurstBubble(){

        Vector3 pos = transform.position;

        GameObject tb1 = Instantiate(tinyBubble, pos, Quaternion.identity);
        GameObject tb2 = Instantiate(tinyBubble, pos, Quaternion.identity);
        GameObject tb3 = Instantiate(tinyBubble, pos, Quaternion.identity);
        GameObject tb4 = Instantiate(tinyBubble, pos, Quaternion.identity);
        
        Destroy(gameObject);

        tb1.GetComponent<TinyBubbleScript>().SetDir(1);
        tb2.GetComponent<TinyBubbleScript>().SetDir(2);
        tb3.GetComponent<TinyBubbleScript>().SetDir(3);
        tb4.GetComponent<TinyBubbleScript>().SetDir(4);

        Destroy(tb1, 4);
        Destroy(tb2, 4);
        Destroy(tb3, 4);
        Destroy(tb4, 4);
    }

    void BurstBubbleTest(){

        Vector3 pos = transform.position;

        int col = (int)(pos.x + 18)/5;
        int line = (int)(pos.y - 5)/(-4);

        int up  = line;
        int down = 5 - up;
        int left = col;
        int right = 4 - left; 

        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");

        foreach (GameObject bubble in bubbles)
        {
            if(up > 0){
                Transform bubbleTrans = bubble.GetComponent<Transform>();
                if(bubbleTrans.position.y == pos.y - (line - up)*5 && bubbleTrans.position.x == pos.x){
                    bubble.GetComponent<BubbleScript>().TouchBubble();
                    up = 0;
                }
                else up--;
            }
            if(down > 0){
                Transform bubbleTrans = bubble.GetComponent<Transform>();
                if(bubbleTrans.position.y == pos.y + (line - down)*5 && bubbleTrans.position.x == pos.x){
                    bubble.GetComponent<BubbleScript>().TouchBubble();
                    down = 0;
                }
                else down--;
            }
            if(left > 0){
                Transform bubbleTrans = bubble.GetComponent<Transform>();
                if(bubbleTrans.position.x == pos.x - (line - left)*5 && bubbleTrans.position.y == pos.y){
                    bubble.GetComponent<BubbleScript>().TouchBubble();
                    left = 0;
                }
                else left--;
            }
            if(right > 0){
                Transform bubbleTrans = bubble.GetComponent<Transform>();
                if(bubbleTrans.position.x == pos.x + (line - right)*5 && bubbleTrans.position.y == pos.y){
                    bubble.GetComponent<BubbleScript>().TouchBubble();
                    right = 0;
                }
                else right--;
            }      
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TouchBubble();
        Destroy(other.gameObject);
    }
}
