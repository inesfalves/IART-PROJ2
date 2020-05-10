using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{

    public Sprite[] sprites;
    private int _bubbleValue = 4;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<SpriteRenderer>().sprite = sprites[_bubbleValue-1]; 
    }

    void OnMouseDown(){
        _bubbleValue = _bubbleValue % 4 + 1;   
    }  
}
