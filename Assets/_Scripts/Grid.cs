using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

    public bool displayGridGizmos;
    public LayerMask unwalkableMask;
    public Vector3 gridWordSize; // 3d space grid
    public Vector3 gridWordSizeInverse; // fraction
    public float nodeRadius;

    public TerrainType[] walkableRegions;
    LayerMask walkableMask;
    Dictionary<int, int> walkableRegionsDictioanry = new Dictionary<int, int>();

    private Node[,] grid;

    private float nodeDiameter;
    private float nodeDiameterInverse;
    private Point3 gridSize;

    void Awake ()
    {
        nodeDiameter = nodeRadius * 2;
        nodeDiameterInverse = 1f / nodeDiameter;
        gridSize.x = Mathf.RoundToInt(gridWordSize.x * nodeDiameterInverse);
        gridSize.z = Mathf.RoundToInt(gridWordSize.z * nodeDiameterInverse);

        gridWordSizeInverse = new Vector3(1f / gridWordSize.x, 1f, 1f / gridWordSize.z);

        foreach(TerrainType region in walkableRegions)
        {
            walkableMask.value = walkableMask |= region.terrainMask.value;
            walkableRegionsDictioanry.Add((int)Mathf.Log(region.terrainMask.value, 2f), region.terrainPenalty);
        }

        CreateGrid();

    }

    public int MaxSize
    {
        get
        {
            return gridSize.x * gridSize.z;
        }
    }

    private void CreateGrid ()
    {
        grid = new Node[gridSize.x, gridSize.z];
        Vector3 worldBottomLeft = transform.position - new Vector3(gridWordSize.x * 0.5f, 0f, gridWordSize.z * 0.5f);


        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.z; z++)
            {
                Vector3 worldPoint = worldBottomLeft + new Vector3(x * nodeDiameter + nodeRadius, 0f, z * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius*.95f, unwalkableMask));
                int movementPentaly = 0;

                if (walkable)
                {
                    Ray ray = new Ray(worldPoint + Vector3.up * 50f, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100f, walkableMask))
                    {
                        walkableRegionsDictioanry.TryGetValue(hit.collider.gameObject.layer, out movementPentaly);
                    }
                }

                grid[x, z] = new Node(walkable, worldPoint, new Point3(x, 0, z), movementPentaly);
            }
        }
    }


    public List<Node> GetNeighbors (Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {

                if (0 == x && 0 == z) continue;
                Point3 check = node.gridPosition + new Point3(x, 0, z);

                if (check.x >= 0 && check.x < gridSize.x && check.z >= 0 && check.z < gridSize.z)
                {
                    //Debug.Log(check.z);
                    neighbors.Add(grid[check.x, check.z]);
                }
            }
        }
        return neighbors;
    }

    public Node NodeFromWorldPoint (Vector3 worldPosition)
    {
        Vector3 percent;
        percent.x = Mathf.Clamp01((worldPosition.x + gridWordSize.x * 0.5f) * gridWordSizeInverse.x);
        percent.z = Mathf.Clamp01((worldPosition.z + gridWordSize.z * 0.5f) * gridWordSizeInverse.z);

        int x = Mathf.RoundToInt((gridSize.x - 1) * percent.x);
        int z = Mathf.RoundToInt((gridSize.z - 1) * percent.z);


        return grid[x, z];
    }



    void OnDrawGizmos ()
    {
        Gizmos.DrawWireCube(transform.position, gridWordSize);
        if (displayGridGizmos && grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable ? Color.white : Color.red);
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.5f));
            }
        }
    }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }

}

