using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Direction direction;

    public enum Direction
    {
        Top,
        Down,
        Left,
        Right,
        None
    }

    
    void Update()
    {
        
    }
}
