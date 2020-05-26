using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyBubbleScript : MonoBehaviour
{
    private Vector2 _velocity;
    // Start is called before the first frame update
    void Awake()
    {
        _velocity = new Vector2(0, 0);
    }

    public void SetDir(int dir)
    {
        // 1-up 2-down 3-left 4-right
        switch (dir)
        {
            case 1:
                _velocity = new Vector2(0, 1);
                break;
            case 2:
                _velocity = new Vector2(0, -1f);
                break;
            case 3:
                _velocity = new Vector2(-1f, 0);
                break;
            case 4:
                _velocity = new Vector2(1f, 0);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(_velocity);
    }
}
