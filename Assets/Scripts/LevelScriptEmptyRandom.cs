﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScriptEmptyRandom : MonoBehaviour
{
    public int maxTouches;

    public GameObject[] level_prefabs;

    private int _touchesLeft;

    // Start is called before the first frame update
    void Start()
    {
        _touchesLeft = maxTouches;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasWon())
        {
            //Application.Quit(); nao funciona com o editor
            //UnityEditor.EditorApplication.isPlaying = false;
        }
        else if (HasLost())
        {
            //Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void UpdateTouches()
    {
        _touchesLeft--;
    }
    
    public void ResetTouches()
    {
        _touchesLeft = maxTouches;
    }

    public bool HasWon()
    {
        return GameObject.FindGameObjectsWithTag("Bubble").Length == 0;
    }

    public bool HasLost()
    {
        return _touchesLeft == 0 && GameObject.FindGameObjectsWithTag("TinyBubble").Length == 0;
    }
    
    public void ResetBubbles()
    {
        GameObject grid = GameObject.FindGameObjectWithTag("Grid");
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        GameObject[] tinies = GameObject.FindGameObjectsWithTag("TinyBubble");

        Destroy(grid);

        foreach(GameObject bubble in bubbles){
            Destroy(bubble);
        }
        foreach(GameObject tiny in tinies){
            Destroy(tiny);
        }

        int level = UnityEngine.Random.Range(0,4);
        if(level == 4){
            maxTouches = 2;
        }else{
            maxTouches = 1;
        }

        ResetTouches();
        Instantiate(level_prefabs[level], new Vector3(0,0,0), Quaternion.identity);
        
    }

}
