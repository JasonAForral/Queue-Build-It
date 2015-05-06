using UnityEngine;
using System.Collections;

public class Builder : Unit {
    public Transform[] jobWaypoints;

    public short JobIndex = 0;

	// Use this for initialization
    //override void Start ()
    //{
    //    //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        
    //}

    protected override void Start ()
    {
        base.Start();
    }
	
	// Update is called once per frame
    protected override void Update ()
    {
        base.Update();

        
        
        //// builder has jobWaypoints
        if (0 < jobWaypoints.Length)
        {
        //    if (JobIndex < jobWaypoints.Length)
        //    {

        //        switch (JobIndex)
        //        {
        //            case 0:
        //                {
        //                    jobWaypoints[JobIndex].parent = transform;
        //                    jobWaypoints[JobIndex].localRotation = Quaternion.identity;
        //                    jobWaypoints[JobIndex].localPosition = new Vector3(0f, 1.1f, 0f);
        //                    JobIndex++;
        //                    //navi.destination = jobWaypoints[JobIndex].position;
        //                    break;
        //                }
        //            case 1:
        //                {
        //                    jobWaypoints[JobIndex - 1].parent = null;
        //                    jobWaypoints[JobIndex - 1].rotation = jobWaypoints[JobIndex].rotation;
        //                    jobWaypoints[JobIndex - 1].position = jobWaypoints[JobIndex].position;
        //                    jobWaypoints[JobIndex].gameObject.SetActive(false);
        //                    JobIndex++;
        //                    //navi.destination = jobWaypoints[JobIndex].position;
        //                    //navi.Stop();
        //                    break;
        //                }
        //            default:
        //                {
        //                    JobIndex++;
        //                    //navi.destination = jobWaypoints[JobIndex].position;
        //                    //navi.Stop();
        //                    break;
        //                }
        //        }
        //    }
        //}
        //else
        //    Debug.Log(navi.remainingDistance);

        //if (Input.GetButton("Fire1"))
        //{
        //    if (ToggleNav)
        //    {
        //        navi.Resume();
        //        ToggleNav = false;
        //    }
        //    else
        //    {
        //        navi.Stop();
        //        ToggleNav = true;
        //    }
        }

    }
}
