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
    public TaskState currentState;

    public LineRenderer lineRenderer;
    
    // Use this for initialization
    protected virtual void Awake ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        selectType = SelectType.Unit;
    }

    protected void StartNewPath (Vector3 targetPosition)
    {
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
            if (debugPathLength > 0)
            {
                currentState = TaskState.Moving;
                GameManager.uiManager.UpdateUI(this);
                StopCoroutine("BuildOrder");
                
                StopCoroutine("FollowPath");
                StopCoroutine("DrawPath");

                StartCoroutine("FollowPath");
                StartCoroutine("DrawPath");
            }
        }
    }

    IEnumerator DrawPath ()
    {
        lineRenderer.enabled = true;
        while (TaskState.Moving == currentState)
        {

            lineRenderer.SetVertexCount(path.Length - pathWaypointIndex + 1);

            for (int i = -1; i < path.Length - pathWaypointIndex; i++)
            {
                if (i == -1)
                    lineRenderer.SetPosition(0, transform.position + Vector3.up * 0.2f);
                else
                    lineRenderer.SetPosition(i + 1, path[i + pathWaypointIndex] + Vector3.up * 0.2f);
            }

            yield return null;
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
                    // path complete
                    EndPath();
                    yield break;
                }
                currentWaypoint = path[pathWaypointIndex];

            }
            transform.LookAt(currentWaypoint, Vector3.up);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, (speed) * Time.deltaTime);

            yield return null;
        }
    }

    protected void EndPath ()
    {
        target = null;
        isPathing = false;
        lineRenderer.enabled = false;
        if (TaskState.Moving == currentState)
            currentState = TaskState.Idle;
        GameManager.uiManager.UpdateUI(this);
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

    public void Move (Vector3 destination)
    {
        //Debug.Log(name + " moves to " + destination);
        StartNewPath(destination);
        GameManager.inputManager.CancelCommand();
    }

    public override string Status ()
    {
        switch (currentState)
        {
        default:
            return currentState.ToString();
        }
    }

    public enum TaskState
    {
        Idle,
        Moving,
        Building,
        DeBuilding,
        Attacking,
        Sleeping
    }

    public enum AssignmentState
    {
        Idle,
        Moving,
        Building,
        DeBuilding,
        Attacking,
        Sleeping
    }
    
}

