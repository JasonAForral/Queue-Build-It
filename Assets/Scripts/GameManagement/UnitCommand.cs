using UnityEngine;
using System.Collections;

public class UnitCommand : MonoBehaviour {
    
    InputManager inputManager;

    void Awake ()
    {
        inputManager = GetComponent<InputManager>();
    }

    public void Move ()
    {
        bool isCommanding = !inputManager.isCommanding;
        // TODO may need to change this or highlight active command

        if (isCommanding)
        {
            inputManager.currentMode = ClickMode.Move;
            Debug.Log("Prompt Move");

            //ClickManager.instance.selectedUnit.GetComponent<Builder>().BuildPrompt();
        }
        else
        {
            inputManager.CancelCommand();
        }
    }

    public void Build ()
    {
        bool isCommanding = !inputManager.isCommanding;
        if (isCommanding)
        {
            inputManager.currentMode = ClickMode.Build;
        }
        else
        {
            inputManager.CancelCommand();
        }
    }

    public void DeBuild ()
    {
        bool isCommanding = !inputManager.isCommanding;
        if (isCommanding)
        {
            inputManager.currentMode = ClickMode.Move;
            Debug.Log("Prompt DeBuild");
            //ClickManager.instance.selectedUnit.GetComponent<Builder>().MoveInput();
            //Build prompt

        }
        else
        {
            inputManager.CancelCommand();
        }
    }

    public void Attack ()
    {
        bool isCommanding = !inputManager.isCommanding;
        if (isCommanding)
        {
            inputManager.currentMode = ClickMode.Move;
            Debug.Log("Command Attack");
            //ClickManager.instance.selectedUnit.GetComponent<Builder>().MoveInput();
            //Build prompt

        }
        else
        {
            inputManager.CancelCommand();
        }
    }
}
