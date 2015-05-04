using UnityEngine;
using System.Collections;

public class BuilderNavMesh : MonoBehaviour {
    private NavMeshAgent navi;
    public Transform[] target;

    public NavMeshPathStatus status = NavMeshPathStatus.PathPartial;
    public bool pathPending;

    public Vector3 debugTarget;

    public short index = 0;

    public bool ToggleNav;

	// Use this for initialization
	void Start () {
        navi = GetComponent<NavMeshAgent>();
        
        navi.destination = target[index].position;
    }
	
	// Update is called once per frame
    void Update ()
    {
        //test_sqrMag = (transform.position - waypoints.position).sqrMagnitude;
        //if ((transform.position - waypoints.position).sqrMagnitude > 4f)
        //else
        //    navi.Stop();
        status = navi.pathStatus;
        pathPending = navi.pathPending;
        debugTarget = navi.destination;

        if (navi.stoppingDistance * 0.5 >= navi.remainingDistance)
        {
            if (index < target.Length)
            {

                switch (index)
                {
                    case 0:
                        {
                            target[index].parent = transform;
                            target[index].localRotation = Quaternion.identity;
                            target[index].localPosition = new Vector3(0f, 1.1f, 0f);
                            index++;
                            navi.destination = target[index].position;
                            break;
                        }
                    case 1:
                        {
                            target[index - 1].parent = null;
                            target[index - 1].rotation = target[index].rotation;
                            target[index - 1].position = target[index].position;
                            target[index].gameObject.SetActive(false);
                            index++;
                            navi.destination = target[index].position;
                            //navi.Stop();
                            break;
                        }
                    default:
                        {
                            index++;
                            navi.destination = target[index].position;
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
