using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {

    public bool walkable;
    public Vector3 worldPosition;
    public Point3 gridPosition;
    public int movementPenalty;

    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;

    public Node (bool walkable, Vector3 worldPosition, Point3 gridPosition, int penalty)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridPosition = gridPosition;
        this.movementPenalty = penalty;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo (Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (0 == compare)
            compare = hCost.CompareTo(nodeToCompare.hCost);
        return -compare;
    }
}
