using UnityEngine;
using System.Collections;

public class MoveCommand : MonoBehaviour {

    public void CommandMove ()
    {
        bool isCommanding = !ClickManager.instance.isCommanding;
        if (isCommanding)
        {
            ClickManager.instance.currentMode = ClickMode.Move;
            Debug.Log("Command");
            //selectedUnit = ClickManager.instance.selectedUnit.GetComponent<Unit>();
            ClickManager.instance.selectedUnit.GetComponent<Unit>().MoveInput();
        }
        else
        {
            ClickManager.instance.CancelCommand();
        }
    }
}
