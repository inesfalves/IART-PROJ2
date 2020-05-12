using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject tinyBubble;
    public int bubbleValue = 4;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(bubbleValue > 0)
            this.GetComponent<SpriteRenderer>().sprite = sprites[bubbleValue-1]; 
    }

    void OnMouseDown(){

        TouchBubble();
    }  

    void TouchBubble()
    {
        bubbleValue--;
        if (bubbleValue == 0)
        {
            BurstBubble();
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        TouchBubble();
        Destroy(other.gameObject);
    }
}
