using UnityEngine;
using System.Collections;

public class Builder : MonoBehaviour {
    public Transform[] waypoints;

    public short index = 0;

	// Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
    void Update ()
    {
        // waypoints
        if (0 < waypoints.Length)
        {
            if (index < waypoints.Length)
            {

                switch (index)
                {
                    case 0:
                        {
                            waypoints[index].parent = transform;
                            waypoints[index].localRotation = Quaternion.identity;
                            waypoints[index].localPosition = new Vector3(0f, 1.1f, 0f);
                            index++;
                            //navi.destination = waypoints[index].position;
                            break;
                        }
                    case 1:
                        {
                            waypoints[index - 1].parent = null;
                            waypoints[index - 1].rotation = waypoints[index].rotation;
                            waypoints[index - 1].position = waypoints[index].position;
                            waypoints[index].gameObject.SetActive(false);
                            index++;
                            //navi.destination = waypoints[index].position;
                            //navi.Stop();
                            break;
                        }
                    default:
                        {
                            index++;
                            //navi.destination = waypoints[index].position;
                            //navi.Stop();
                            break;
                        }
                }
            }
        }
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
        //}

    }
}
