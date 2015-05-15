using UnityEngine;
using System.Collections;

public class Builder : Unit
{
    public Transform[] jobWaypoints;

    public short JobIndex = 0;

    public float progress;

    public override string Status
    {
        get
        {
            switch (currentState)
            {
            case UnitState.Building:
            case UnitState.DeBuilding:
                return currentState + ": " + progress.ToString("F3");
            default:
                return base.Status;
            }
        }
    }


    protected override void Start ()
    {
        base.Start();
    }

    protected override void Update ()
    {
        base.Update();

        // builder has jobWaypoints
        if (0 < jobWaypoints.Length)
        {

        }

    }

    public void BuildPrompt ()
    {
        Debug.Log("Where should " + name + " build?");
    }

    public void Build (GameObject target)
    {
        StopCoroutine("BuildJob");
        StartCoroutine("BuildJob", target);
        InputManager.instance.CancelCommand();
    }

    IEnumerator BuildJob (GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Structure targetStructure = target.GetComponent<Structure>();
        StopCoroutine("FollowPath");
        StopCoroutine("DrawPath");
        
        StartNewPath(targetPosition);
        while ((transform.position - targetPosition).sqrMagnitude > 1f)
        {
            yield return null;
        }

        StopCoroutine("FollowPath");
        StopCoroutine("DrawPath");
        currentState = UnitState.Building;
        EndPath();
        // do build task
        //UIManager.instance.UpdateUI(this);
                    

        while (targetStructure.buildProgress < targetStructure.totalBuildCost)
        {
            progress = targetStructure.totalBuildCost-targetStructure.buildProgress;
            UIManager.instance.UpdateUI(this);
            if (UnitState.Building != currentState)
            {
                Debug.Log(currentState.ToString());
                yield break;
            }
            targetStructure.Build(1f * Time.deltaTime);
            yield return null;
        }

        TaskManager.instance.MakeWall(target);
        Debug.Log("Job's Done");
        currentState = UnitState.Idle;
        UIManager.instance.UpdateUI(this);
                    

    }


    //IEnumerator AssembleStructure (Structure target)
    //{
    //    currentState = UnitState.Building;
    //        target.Build(1f * Time.deltaTime);
    //}
}
