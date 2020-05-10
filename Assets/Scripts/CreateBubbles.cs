using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBubbles : MonoBehaviour
{
    Vector2 gridSize;
    GameObject[][] gridOfGameObjects;
    // Start is called before the first frame update
    void Start()
    { 
        gridSize = new Vector2(5, 6);
         gridOfGameObjects = new GameObject[(int)gridSize.x][];
         for (int x = 0; x < gridSize.x; x++)
         {
             gridOfGameObjects[x] = new GameObject[(int)gridSize.y];
             for (int y = 0; y < gridSize.y; y++)
             {
                 GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                 // manipulate gameobject here
                 go.transform.position = new Vector3(x*2,y*2,0);
                 gridOfGameObjects[x][y] = go;
             }
         }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
