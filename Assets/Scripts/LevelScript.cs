using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public readonly int maxTouches;
    private int _touchesLeft;

    // Start is called before the first frame update
    void Start()
    {
        _touchesLeft = maxTouches;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTouches()
    {
        _touchesLeft--;
    }
}
