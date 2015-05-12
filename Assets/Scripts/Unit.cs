using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit : SelectableObject
{

    public Transform target;
    public float speed = 10f;
    Vector3[] path;
    public int pathWaypointIndex;
    public bool isPathing;
    public int debugPathLength;
    
    
    // Use this for initialization
    protected override void Awake ()
    {
        guiPanel = GameObject.FindGameObjectWithTag(Tags.GUIUnit);
        guiTextDisplay = guiPanel.GetComponentInChildren<Text>();
    }

    protected override void Start ()
    { }
    protected override void Update ()
    {


        //if (!isPathing && target != null)
        //{
        //    StartNewPath();
        //}
        //else 
        //{

        //}
        
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
                Gizmos.color = Color.green;
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

    public void MoveInput ()
    {
        Debug.Log("Where should " + name + " move?");
    }

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
