using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit : SelectableObject
{

    public Transform target;
    public float speed = 10f;
    [SerializeField]
    Vector3[] path;
    public int pathWaypointIndex = 0;
    public bool isPathing;
    public int debugPathLength;
    public UnitState currentState;

    public LineRenderer lineRenderer;
    
    // Use this for initialization
    protected override void Start ()
    {
        guiPanel = HashIDs.GuiUnit;
        guiTextDisplay = HashIDs.UnitText;

        lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void Update ()
    {


        //if (!isPathing && target != null)
        //{
        //    StartNewPath();
        //}
        //else 
        //{

        //}
        if (null != path && isPathing)
        {
            lineRenderer.SetVertexCount(path.Length - pathWaypointIndex+1);

            for (int i = -1; i < path.Length - pathWaypointIndex; i++)
            {
                if (i == -1)
                //{
                lineRenderer.SetPosition(0, transform.position);
                //}
                else
                //{
                lineRenderer.SetPosition(i +1, path[i+pathWaypointIndex]);
                //}
                //Debug.Log((i - pathWaypointIndex + 1) + ", " + path.Length);
            }
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, path[path.Length - 1]);
        }
        else
        {
            lineRenderer.SetVertexCount(0);
        }
    }

    void StartNewPath (Vector3 targetPosition)
    {
        //Debug.Log("go");

        PathRequestManager.RequestPath(transform.position, targetPosition, OnPathFound);
        isPathing = true;
    }

    public void OnPathFound (Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            //pathWaypointIndex = 0;
            path = newPath;
            debugPathLength = path.Length;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");

        }
    }

    IEnumerator FollowPath ()
    {
        pathWaypointIndex = 0;
        Vector3 currentWaypoint = path[0];

        while (true)
        {

            
            if (transform.position == currentWaypoint)
            {
                
                pathWaypointIndex++;

                if (pathWaypointIndex >= path.Length)
                {
                    target = null;
                    isPathing = false;
                    yield break;
                }
                currentWaypoint = path[pathWaypointIndex];

            }
            transform.LookAt(currentWaypoint, Vector3.up);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, (speed) * Time.deltaTime);
            yield return null;
        }
    }


    public void OnDrawGizmos ()
    {
        if (null != path)
        {
            
            for (int i = pathWaypointIndex; i < path.Length; i++)
            {
                
                Gizmos.color = Color.blue;
                //Gizmos.DrawCube(path[i], Vector3.one);
                Gizmos.DrawWireSphere(path[i], 0.5f);
                if (i == pathWaypointIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    //public void MoveInput ()
    //{
    //    Debug.Log("Where should " + name + " move?");
    //}

    public void Move (Vector3 destination)
    {
        //Debug.Log(name + " moves to " + destination);
        StartNewPath(destination);
        ClickManager.instance.CancelCommand();
    }

    //public void Select ()
    //{
    //}

    public override void DisplayUI ()
    {
        guiTextDisplay.text = "Unit: " + name;
        base.DisplayUI();
    }

    
}

public enum UnitState
{
    Idle,
    Moving,
    Building,
    DeBuilding,
    Attacking,
    Sleeping
}