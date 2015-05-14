using UnityEngine;
using System.Collections;

public class UnitCommand : MonoBehaviour {

    

    public void Move ()
    {
        bool isCommanding = !ClickManager.instance.isCommanding;
        // TODO may need to change this or highlight active command

        if (isCommanding)
        {
            ClickManager.instance.currentMode = ClickMode.Move;
            Debug.Log("Prompt Move");

            //ClickManager.instance.selectedUnit.GetComponent<Builder>().BuildPrompt();
        }
        else
        {
            ClickManager.instance.CancelCommand();
        }
    }

    public void Build ()
    {
        bool isCommanding = !ClickManager.instance.isCommanding;
        if (isCommanding)
        {
            ClickManager.instance.currentMode = ClickMode.Build;
            Debug.Log("Prompt Build");
            //ClickManager.instance.selectedUnit.GetComponent<Builder>().MoveInput();

            //Build prompt

        }
        else
        {
            ClickManager.instance.CancelCommand();
        }
    }

    public void DeBuild ()
    {
        bool isCommanding = !ClickManager.instance.isCommanding;
        if (isCommanding)
        {
            ClickManager.instance.currentMode = ClickMode.Move;
            Debug.Log("Prompt DeBuild");
            //ClickManager.instance.selectedUnit.GetComponent<Builder>().MoveInput();
            //Build prompt

        }
        else
        {
            ClickManager.instance.CancelCommand();
        }
    }

    public void Attack ()
    {
        bool isCommanding = !ClickManager.instance.isCommanding;
        if (isCommanding)
        {
            ClickManager.instance.currentMode = ClickMode.Move;
            Debug.Log("Command Attack");
            //ClickManager.instance.selectedUnit.GetComponent<Builder>().MoveInput();
            //Build prompt

        }
        else
        {
            ClickManager.instance.CancelCommand();
        }
    }
}
