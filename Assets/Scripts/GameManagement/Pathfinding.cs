﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;


public class Pathfinding : MonoBehaviour
{
    PathRequestManager requestManager;
    Grid grid;

    void Awake ()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath (Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }


    IEnumerator FindPath (Vector3 startPosition, Vector3 targetPosition)
    {

        // check if nodes are the same;


        
        //Stopwatch sw = new Stopwatch();
        //sw.Start();

        Vector3[] waypoints = new Vector3[0];

        bool pathSuccess = false;

        Node startNode = grid.WorldToNode(startPosition);
        Node targetNode = grid.WorldToNode(targetPosition);

        if (startNode.walkable && targetNode.walkable && (startNode != targetNode))
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);


            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {

                    //sw.Stop();
                    //print("Path Found: " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;
                    RetracePath(startNode, targetNode);
                    break;
                }


                foreach (Node neighbor in grid.GetNeighbors(currentNode))
                {

                    if (!neighbor.walkable || closedSet.Contains(neighbor)) continue;

                    int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor) + neighbor.movementPenalty;

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
        
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        else
        {
            waypoints = RetracePath(startNode, startNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath (Node beginNode, Node endNode)
    {
        List<Node> path = new List<Node>();

        Node currentNode = endNode;

        while (currentNode != beginNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        System.Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath (List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector3 directionOld = Vector3.zero;

        if (path.Count > 1)
        {
            for (int i = 0; i < path.Count; i++)
            {
                int j = i + 1;
                if (i >= path.Count - 1) j = i;
                Vector3 directionNew = (path[j].gridPosition - path[i].gridPosition).toVector3;
                if (directionNew != directionOld)
                {
                    waypoints.Add(path[i].worldPosition);
                }
                directionOld = directionNew;
            }
        }
        else if (path.Count == 1) 
        {
            waypoints.Add(path[0].worldPosition);
        }
        return waypoints.ToArray();
    }

    int GetDistance (Node nodeA, Node nodeB)
    {
        Point3 distance = (nodeA.gridPosition - nodeB.gridPosition).abs;
        if (distance.x > distance.z)
        {
            return 14 * distance.z + 10 * (distance.x - distance.z);
        }

        return 14 * distance.x + 10 * (distance.z - distance.x);

    }
}
   