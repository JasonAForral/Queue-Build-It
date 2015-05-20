using UnityEngine;
using System.Collections;

public class Builder : Unit
{
    public Transform[] jobWaypoints;

    public short JobIndex = 0;

    public float progress;

    public override string Status ()
    {
        switch (currentState)
        {
        case TaskState.Building:
        case TaskState.DeBuilding:
            return currentState + ": " + progress.ToString("F3");
        case TaskState.Moving:
            return currentState + " to assignment";
        default:
            return base.Status();
        }
    }

    private void RequestAssignment ()
    {

    }


    public void BuildPrompt ()
    {
        Debug.Log("Where should " + name + " build?");
    }

    public void Build (GameObject target)
    {
        StopCoroutine("BuildJob");
        StartCoroutine("BuildJob", target);
        GameManager.inputManager.CancelCommand();
    }

    IEnumerator BuildJob (GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Structure targetStructure = target.GetComponent<Structure>();
        StopCoroutine("FollowPath");
        StopCoroutine("DrawPath");
        
        StartNewPath(targetPosition);
        yield return null;
        while ((transform.position - targetPosition).sqrMagnitude > 1f)
        {
            yield return null;
        }
        
        StopCoroutine("FollowPath");
        StopCoroutine("DrawPath");
        currentState = TaskState.Building;
        EndPath();
        yield return null;
        // do build task
        //UIManager.instance.UpdateUI(this);
                    

        while (targetStructure.buildProgress < targetStructure.totalBuildCost)
        {
            if (TaskState.Building != currentState)
            {
                Debug.Log(currentState.ToString());
                yield break;
            }
            progress = targetStructure.totalBuildCost - targetStructure.buildProgress;
            GameManager.uiManager.UpdateUI(this);
            targetStructure.Build(1f * Time.deltaTime);
            yield return null;
        }

        GameManager.taskManager.MakeWall(target);
        Debug.Log("Job's Done");
        currentState = TaskState.Idle;
        GameManager.uiManager.UpdateUI(this);
    }

}
