using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private bool displayGridGizmos;
    [SerializeField]
    private LayerMask unwalkableMask;
    [SerializeField]
    private Vector3 gridWordSize; // 3d space grid
    [SerializeField]
    private float nodeRadius;

    [SerializeField]
    private TerrainType[] walkableRegions;
    
    private Vector3 gridWordSizeInverse; // fraction

    private LayerMask walkableMask;
    private Dictionary<int, int> walkableRegionsDictioanry = new Dictionary<int, int>();

    private Node[,] grid;

    private float nodeDiameter;
    private float nodeDiameterInverse;
    private Point3 gridSize;

    private Vector3 mNodeToWorld;
    private Vector3 bIntercept;
    private Vector3 mWorldToNode;


    void Awake ()
    {
        SetCoefficents();

        foreach (TerrainType region in walkableRegions)
        {
            walkableMask.value = walkableMask |= region.terrainMask.value;
            walkableRegionsDictioanry.Add((int)Mathf.Log(region.terrainMask.value, 2f), region.terrainPenalty);
        }


        CreateGrid();

    }

    void SetCoefficents ()
    {
        nodeDiameter = nodeRadius * 2;
        nodeDiameterInverse = 1f / nodeDiameter;
        
        gridSize.x = Mathf.RoundToInt(gridWordSize.x * nodeDiameterInverse);
        gridSize.z = Mathf.RoundToInt(gridWordSize.z * nodeDiameterInverse);

        gridWordSizeInverse = new Vector3(1f / gridWordSize.x, 1f, 1f / gridWordSize.z);

        for (int i = 0; i < 3; i += 2)
        {
            mNodeToWorld[i] = gridWordSize[i] / (float)gridSize[i];
            bIntercept[i] = gridWordSize[i] * 0.5f - nodeRadius;

            mWorldToNode[i] = gridSize[i] * gridWordSizeInverse[i];
        }
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
        //Vector3 worldBottomLeft = transform.position - new Vector3(gridWordSize.x * 0.5f - nodeRadius, 0f, gridWordSize.z * 0.5f - nodeRadius);


        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.z; z++)
            {
                //Vector3 worldPoint = worldBottomLeft + new Vector3(x * nodeDiameter - nodeRadius, 0f, z * nodeDiameter - nodeRadius);
                Vector3 worldPoint = NodeToWorld(x, z);
                
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

    //public Node WorldPointToNode (Vector3 worldPosition)
    //{
    //    Vector3 percent;
    //    percent.x = Mathf.Clamp01((worldPosition.x + gridWordSize.x * 0.5f - nodeRadius) * gridWordSizeInverse.x);
    //    percent.z = Mathf.Clamp01((worldPosition.z + gridWordSize.z * 0.5f - nodeRadius) * gridWordSizeInverse.z);

    //    int x = Mathf.RoundToInt(gridSize.x * percent.x);
    //    int z = Mathf.RoundToInt(gridSize.z * percent.z);


    //    return grid[x, z];
    //}

    public Vector3 NodeCoordToWorldPoint (int x, int z)
    {
        return new Vector3();
    }



    void OnDrawGizmos ()
    {
        Gizmos.DrawWireCube(transform.position, gridWordSize);
        if (displayGridGizmos && grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable ? Color.white : Color.red);
                Gizmos.DrawWireCube(n.worldPosition + Vector3.down * transform.position.y , new Vector3(nodeDiameter, 0.2f, nodeDiameter));
            }
        }
    }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }

    


    public Node WorldToNode (Vector3 world)
    {
        int x, z;
        x = Mathf.RoundToInt(Mathf.Clamp((world.x + bIntercept.x) * mWorldToNode.x, 0, gridSize.x - 1));
        z = Mathf.RoundToInt(Mathf.Clamp((world.z + bIntercept.z) * mWorldToNode.z, 0, gridSize.z - 1));
        //Debug.Log(x + ", " + z);
        return grid[x, z];
    }

    public Vector3 NodeToWorld (int x, int z)
    {
        Vector3 worldPoint = new Vector3() ;
        worldPoint.x = x * mNodeToWorld.x - bIntercept.x;
        worldPoint.y = transform.position.y;
        worldPoint.z = z * mNodeToWorld.z - bIntercept.z;
        return worldPoint;
    }

    public Vector3 NodeToWorld (Point3 coord)
    {
        return NodeToWorld(coord.x, coord.z);
    }
}

