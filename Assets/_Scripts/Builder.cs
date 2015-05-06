using UnityEngine;
using System.Collections;

public class Builder : Unit {
    public Transform[] jobWaypoints;

    public short index = 0;

	// Use this for initialization
    //override void Start ()
    //{
    //    //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        
    //}

    public override void Start ()
    {
        base.Start();
    }
	
	// Update is called once per frame
    public override void Update ()
    {
        base.Update();

        
        
        //// builder has jobWaypoints
        if (0 < jobWaypoints.Length)
        {
        //    if (index < jobWaypoints.Length)
        //    {

        //        switch (index)
        //        {
        //            case 0:
        //                {
        //                    jobWaypoints[index].parent = transform;
        //                    jobWaypoints[index].localRotation = Quaternion.identity;
        //                    jobWaypoints[index].localPosition = new Vector3(0f, 1.1f, 0f);
        //                    index++;
        //                    //navi.destination = jobWaypoints[index].position;
        //                    break;
        //                }
        //            case 1:
        //                {
        //                    jobWaypoints[index - 1].parent = null;
        //                    jobWaypoints[index - 1].rotation = jobWaypoints[index].rotation;
        //                    jobWaypoints[index - 1].position = jobWaypoints[index].position;
        //                    jobWaypoints[index].gameObject.SetActive(false);
        //                    index++;
        //                    //navi.destination = jobWaypoints[index].position;
        //                    //navi.Stop();
        //                    break;
        //                }
        //            default:
        //                {
        //                    index++;
        //                    //navi.destination = jobWaypoints[index].position;
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
