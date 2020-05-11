using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyBubbleScript : MonoBehaviour
{

    // 1-up 2-down 3-left 4-right
    private int _direction;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetDir(int dir)
    {
        _direction = dir;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_direction)
        {
            case 1:
                gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
                break;
            case 2:
                gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
                break;
            case 3:
                gameObject.transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
                break;
            case 4:
                gameObject.transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
                break;
            default:
                break;
        }
    }
}
