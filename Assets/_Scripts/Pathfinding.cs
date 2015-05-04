using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


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
        if (Input.GetButton("Jump"))
            FindPath(seeker.position, target.position);
    }

    void FindPath (Vector3 startPosition, Vector3 targetPosition)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Node startNode = grid.NodeFromWorldPoint(startPosition);
        Node targetNode = grid.NodeFromWorldPoint(targetPosition);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);


        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {

                RetracePath(startNode, targetNode);
                sw.Stop();
                print("Path Found: " + sw.ElapsedMilliseconds + " ms");
                return;
            }


            foreach (Node neighbor in grid.GetNeighbors(currentNode))
            {

                if (!neighbor.walkable || closedSet.Contains(neighbor)) continue;

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);

                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;
                    //Debug.Log(currentNode.gridPosition);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbor);
                    }
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
            return 1414 * distance.z + 1000 * (distance.x - distance.z);
        }

        return 1414 * distance.x + 1000 * (distance.z - distance.x);
    }
}
   