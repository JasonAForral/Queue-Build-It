using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Structure : SelectableObject, IBuildable<float>, IDeBuildable<float>
{

    // Use this for initialization
    protected override void Start ()
    {
        guiPanel = HashIDs.GuiStructure;
        guiTextDisplay = HashIDs.StructureText;

    }

    // Update is called once per frame
    protected override void Update ()
    {

    }

    public override void DisplayUI ()
    {
        guiTextDisplay.text = "Structure: " + name;
        base.DisplayUI();

    }

    public void Build (float amountBuilt) { }

    public void DeBuild (float amountDeBuilt) { }
}

public enum StructureState
{
    Off,
    Idle,
    Attacking,
}
