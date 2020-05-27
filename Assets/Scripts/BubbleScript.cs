using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            BurstBubble();
        }

        _levelScript.GetComponent<LevelScriptEmptyRandom>().UpdateTouches();
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

        Destroy(tb1, 3);
        Destroy(tb2, 3);
        Destroy(tb3, 3);
        Destroy(tb4, 3);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        TouchBubble();
        Destroy(other.gameObject);
    }
}
