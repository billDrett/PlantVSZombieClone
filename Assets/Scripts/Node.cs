using UnityEngine;
using System.Collections;

public class Node
{
    public Vector3 worldPoint;
    public bool walkable;

    public Node(Vector3 worldPoint, bool walkable)
    {
        this.worldPoint = worldPoint;
        this.walkable = walkable; 
    }
}
