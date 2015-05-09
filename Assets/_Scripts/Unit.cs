using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit : MonoBehaviour, ISelectable, IDamageable<float>
{

    public Transform target;
    public float speed = 10f;
    Vector3[] path;
    public int pathWaypointIndex;
    public bool isPathing;
    public int debugPathLength;
    
    private GameObject guiPanel;
    private Text guiTextDisplay;
    
    // Use this for initialization
    protected virtual void Awake ()
    {
        guiPanel = GameObject.FindGameObjectWithTag("GUIUnit");
        guiTextDisplay = guiPanel.GetComponentInChildren<Text>();
    }

    protected virtual void Start ()
    { }
    protected virtual void Update ()
    {


        if (!isPathing && target != null)
        {
            StartNewPath();
        }
        else 
        {

        }
        
    }

    void StartNewPath ()
    {
        Debug.Log("go");
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
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
                Gizmos.color = Color.magenta;
                Gizmos.DrawCube(path[i], Vector3.one);
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

    public void Select ()
    {
    }

    public void DisplayUI ()
    {
        guiTextDisplay.text = "Unit: " + name;
        guiPanel.SetActive(true);
    }

    public void Damage (float damageTaken)
    {
    }
    
}
