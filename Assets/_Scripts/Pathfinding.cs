using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Pathfinding : MonoBehaviour
{

    public Transform seeker, target;
    
    Grid grid;

    void Awake ()
    {
        grid = GetComponent<Grid>();
    }

    void Update ()
    {
        
        FindPath(seeker.position, target.position);
    }

    void FindPath (Vector3 startPosition, Vector3 targetPosition)
    {
        Node startNode = grid.NodeFromWorldPoint(startPosition);
        Node targetNode = grid.NodeFromWorldPoint(targetPosition);
        
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

     
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    //Debug.Log(i +" -_- " + openSet.Count + " _-_ " + currentNode.gridPosition);
                    //Debug.Break();
                    currentNode = openSet[i];
                }
            }
            
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
             
                RetracePath(startNode, targetNode);
                openSet.Clear();
                closedSet.Clear();
                return;
            }
        
                
            foreach (Node neighbor in grid.GetNeighbors(currentNode))
            {
                //Debug.Log(currentNode.gridPosition + " to " + neighbor.gridPosition);
                //Debug.Break();
        
                if (!neighbor.walkable || closedSet.Contains(neighbor)) continue;

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);

                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;
                    //Debug.Log(currentNode.gridPosition);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }

            }
        }
    }

    void RetracePath (Node beginNode, Node endNode)
    {
        List<Node> path = new List<Node>();

        Node currentNode = endNode;

        while (currentNode != beginNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;

    }

    int GetDistance (Node nodeA, Node nodeB)
    {
        //Debug.Log(nodeA.gridPosition + " to " + nodeB.gridPosition);
        //Debug.Break();
        Point3 distance = new Point3(
            Mathf.Abs(nodeA.gridPosition.x - nodeB.gridPosition.x),
            0,
            Mathf.Abs(nodeA.gridPosition.z - nodeB.gridPosition.z));

        
        //Point3 distance = (nodeA.gridPosition - nodeB.gridPosition).abs;
        if (distance.x > distance.z)
        {
            return 14 * distance.z + 10 * (distance.x - distance.z);
        }

        return 14 * distance.x + 10 * (distance.z - distance.x);

        

    }
}
   