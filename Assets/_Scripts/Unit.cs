using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    public Transform target;
    public float speed = 10f;
    Vector3[] path;
    int targetIndex;

    // Use this for initialization
    void Start ()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound (Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath ()
    {
        targetIndex = 0;
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, (speed) * Time.deltaTime);
            yield return null;
        }
    }
    public void OnDrawGizmos ()
    {
        if (null != path)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i == targetIndex)
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
}
