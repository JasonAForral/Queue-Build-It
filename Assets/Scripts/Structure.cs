using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Structure : SelectableObject, IBuildable<float>, IDeBuildable<float>
{

    public override string Status ()
    {
        if (buildProgress != 0)
            return "Progress: " + (totalBuildCost - buildProgress).ToString("F3");
        else
            return base.Status();

    }


    // Use this for initialization
    protected virtual void Awake ()
    {
        selectType = SelectType.Structure;
    }

    public float totalBuildCost = 3f;
    public float buildProgress;

    public float totalDeBuildCost = 3f;
    public float deBuildProgress;

    public void Build (float amountBuilt)
    {
        buildProgress += amountBuilt;
        GameManager.uiManager.UpdateUI(this);
    }

    public void DeBuild (float amountDeBuilt)
    {
        deBuildProgress += amountDeBuilt;
        GameManager.uiManager.UpdateUI(this);
    }

    public enum StructureState
    {
        Off,
        Idle,
        Attacking,
    }
}

