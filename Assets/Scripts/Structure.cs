using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Structure : SelectableObject, IConstructable<float>, IDestructable<float>
{

    // Use this for initialization
    protected override void Awake ()
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

    public void Destruct (float amountDeconstructed) { }

    public void Construct (float amountDeconstructed) { }
}
