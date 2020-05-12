using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public int maxTouches;
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
            print("you win :D");
            //Application.Quit(); nao funciona com o editor
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else if (_touchesLeft == 0 && (GameObject.FindGameObjectsWithTag("TinyBubble") == null || GameObject.FindGameObjectsWithTag("TinyBubble").Length == 0))
        {
            print("you lost :(");
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void UpdateTouches()
    {
        _touchesLeft--;
    }

    private bool HasWon()
    {
        return (GameObject.FindGameObjectsWithTag("Bubble") == null || GameObject.FindGameObjectsWithTag("Bubble").Length == 0) && (GameObject.FindGameObjectsWithTag("TinyBubble") == null || GameObject.FindGameObjectsWithTag("TinyBubble").Length == 0);
    }
}
