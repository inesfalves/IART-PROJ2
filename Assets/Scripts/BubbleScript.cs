using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject tinyBubble;
    private int _bubbleValue = 4;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(_bubbleValue > 0)
        this.GetComponent<SpriteRenderer>().sprite = sprites[_bubbleValue-1]; 
    }

    void OnMouseDown(){

        _bubbleValue--;
        if(_bubbleValue == 0){
            burstBubble();
        }  
    }  

    void burstBubble(){

        GameObject tb1 = Instantiate(tinyBubble, transform.position, Quaternion.identity);
        GameObject tb2 = Instantiate(tinyBubble, transform.position, Quaternion.identity);
        GameObject tb3 = Instantiate(tinyBubble, transform.position, Quaternion.identity);
        GameObject tb4 = Instantiate(tinyBubble, transform.position, Quaternion.identity);

        tb1.GetComponent<TinyBubbleScript>().SetDir(1);
        tb2.GetComponent<TinyBubbleScript>().SetDir(2);
        tb3.GetComponent<TinyBubbleScript>().SetDir(3);
        tb4.GetComponent<TinyBubbleScript>().SetDir(4);

        Destroy(gameObject);
    }

}
