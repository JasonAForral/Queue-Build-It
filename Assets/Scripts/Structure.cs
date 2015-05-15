using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Structure : SelectableObject, IBuildable<float>, IDeBuildable<float>
{

    public override string Status
    {
        get
        {
            if (buildProgress != 0)
                return "Progress: " + (totalBuildCost - buildProgress).ToString("F3");
            else
                return base.Status;
        }
    }


    // Use this for initialization
    protected override void Awake ()
    {
        selectType = SelectType.Structure;
    }

    // Update is called once per frame
    protected override void Update ()
    {

    }

    public float totalBuildCost = 3f;
    public float buildProgress;

    public float totalDeBuildCost = 3f;
    public float deBuildProgress;

    public void Build (float amountBuilt)
    {
        buildProgress += amountBuilt;
        if (this == InputManager.instance.selectedUnit)
            UIManager.instance.UpdateUI(this);
    }

    public void DeBuild (float amountDeBuilt)
    {
        buildProgress += amountDeBuilt;
    }

}

public enum StructureState
{
    Off,
    Idle,
    Attacking,
}
