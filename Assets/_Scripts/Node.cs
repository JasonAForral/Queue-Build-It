using UnityEngine;
using System.Collections;

public class Node {

    public bool walkable;
    public Vector3 worldPosition;
    public Point3 gridPosition;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node (bool walkable, Vector3 worldPosition, Point3 gridPosition)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridPosition = gridPosition;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    
}
