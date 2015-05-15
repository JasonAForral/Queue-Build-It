using UnityEngine;
using System.Collections;

public class UnitCommand : MonoBehaviour {

    

    public void Move ()
    {
        bool isCommanding = !InputManager.instance.isCommanding;
        // TODO may need to change this or highlight active command

        if (isCommanding)
        {
            InputManager.instance.currentMode = ClickMode.Move;
            Debug.Log("Prompt Move");

            //ClickManager.instance.selectedUnit.GetComponent<Builder>().BuildPrompt();
        }
        else
        {
            InputManager.instance.CancelCommand();
        }
    }

    public void Build ()
    {
        bool isCommanding = !InputManager.instance.isCommanding;
        if (isCommanding)
        {
            InputManager.instance.currentMode = ClickMode.Build;
        }
        else
        {
            InputManager.instance.CancelCommand();
        }
    }

    public void DeBuild ()
    {
        bool isCommanding = !InputManager.instance.isCommanding;
        if (isCommanding)
        {
            InputManager.instance.currentMode = ClickMode.Move;
            Debug.Log("Prompt DeBuild");
            //ClickManager.instance.selectedUnit.GetComponent<Builder>().MoveInput();
            //Build prompt

        }
        else
        {
            InputManager.instance.CancelCommand();
        }
    }

    public void Attack ()
    {
        bool isCommanding = !InputManager.instance.isCommanding;
        if (isCommanding)
        {
            InputManager.instance.currentMode = ClickMode.Move;
            Debug.Log("Command Attack");
            //ClickManager.instance.selectedUnit.GetComponent<Builder>().MoveInput();
            //Build prompt

        }
        else
        {
            InputManager.instance.CancelCommand();
        }
    }
}
