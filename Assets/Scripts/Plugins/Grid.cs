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

    private float nodeRadiusHalf;
    private float nodeDiameter;
    private float nodeDiameterInverse;
    private Point3 gridSize;

    private Vector3 mNodeToWorld;
    private Vector3 bIntercept;
    private Vector3 mWorldToNode;

    [SerializeField]
    private bool _hexGrid;
    [SerializeField]
    private float _hexFactor;
    private float hexFactorInverse;

    public bool hexGrid { get { return _hexGrid; } }
    public float hexFactor { get { return _hexFactor; } }



    void Awake ()
    {
        SetCoefficents();

        foreach (TerrainType region in walkableRegions)
        {
            walkableMask.value = walkableMask |= region.mask.value;
            walkableRegionsDictioanry.Add((int)Mathf.Log(region.mask.value, 2f), region.penalty);
        }


        CreateGrid();

        Debug.Log("Grid ready");

    }

    void SetCoefficents ()
    {
        _hexFactor = Mathf.Sin(60 * Mathf.Deg2Rad);
        hexFactorInverse = 1f / hexFactor;

        nodeRadiusHalf = nodeRadius * 0.5f;
        nodeDiameter = nodeRadius * 2;
        nodeDiameterInverse = 1f / nodeDiameter;

        gridSize.x = Mathf.RoundToInt(gridWordSize.x * nodeDiameterInverse);
        
        if (hexGrid)
        {
            gridSize.z = Mathf.RoundToInt(gridWordSize.z * nodeDiameterInverse * hexFactorInverse);
            gridWordSize.z = gridSize.z * nodeDiameter * hexFactor;
        }
        else
        {
            gridSize.z = Mathf.RoundToInt(gridWordSize.z * nodeDiameterInverse);
            gridWordSize.z = gridSize.z * nodeDiameter;
        }


        gridWordSize.x = gridSize.x * nodeDiameter;



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

                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius * .95f, unwalkableMask));
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

                if (hexGrid)
                {
                    if (isEvenZ(node.gridPosition.z))
                    {
                        if (0 > x && 0 != z) continue;
                    }
                    else if (0 < x && 0 != z) continue;

                }
                Point3 check = node.gridPosition + new Point3(x, 0, z);

                if (check.x >= 0 && check.x < gridSize.x && check.z >= 0 && check.z < gridSize.z)
                {
                    neighbors.Add(grid[check.x, check.z]);
                }
            }
        }
        return neighbors;
    }

    public void ChangeWalkableNode (Vector3 position)
    {
        Node nodeToChange = WorldToNode(position);
        nodeToChange.walkable = !nodeToChange.walkable;
    }

    void OnDrawGizmos ()
    {
        Gizmos.DrawWireCube(transform.position, gridWordSize);
        if (displayGridGizmos && grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable ? Color.grey : Color.red);
                Gizmos.DrawSphere(n.worldPosition + Vector3.up * (transform.position.y), nodeRadius);
            }
        }
    }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask mask;
        public int penalty;
    }

    public Node WorldToNode (Vector3 worldPoint)
    {
        int x, z;
        x = Mathf.RoundToInt(Mathf.Clamp((worldPoint.x + bIntercept.x) * mWorldToNode.x, 0, gridSize.x - 1));
        z = Mathf.RoundToInt(Mathf.Clamp((worldPoint.z + bIntercept.z) * mWorldToNode.z, 0, gridSize.z - 1));
        //Debug.Log(x + ", " + z);
        return grid[x, z];
    }

    private Vector3 NodeToWorld (int x, int z)
    {
        Vector3 worldPoint = new Vector3();
        worldPoint.x = x * mNodeToWorld.x - bIntercept.x;
        worldPoint.y = transform.position.y;
        worldPoint.z = z * mNodeToWorld.z - bIntercept.z;

        if (hexGrid)
        {
            worldPoint.x += EvenOddOffset(z);
        }

        return worldPoint;
    }

    private static bool isEvenZ (int z)
    {
        return (z % 2 == 0);
    }

    private float EvenOddOffset (int z)
    {
        if (isEvenZ(z))
            return nodeRadiusHalf;
        else
            return -nodeRadiusHalf;
    }

    public Vector3 NodeToWorld (Point3 coord)
    {
        return NodeToWorld(coord.x, coord.z);
    }
}
