using UnityEngine;
using System.Collections;

public class BuildCommand : MonoBehaviour {

    public void CommandMove ()
    {
        bool isCommanding = !ClickManager.instance.isCommanding;
        // TODO may need to change this or highlight active command

        if (isCommanding)
        {
            ClickManager.instance.currentMode = ClickMode.Build;
            Debug.Log("Command Build");

            ClickManager.instance.selectedUnit.GetComponent<Builder>().BuildInput();
        }
        else
        {
            ClickManager.instance.CancelCommand();
        }
    }

    public void CommandBuild ()
    {
        bool isCommanding = !ClickManager.instance.isCommanding;
        if (isCommanding)
        {
            ClickManager.instance.currentMode = ClickMode.Build;
            Debug.Log("Command Build");
            ClickManager.instance.selectedUnit.GetComponent<Builder>().MoveInput();
        }
        else
        {
            ClickManager.instance.CancelCommand();
        }
    }
}
