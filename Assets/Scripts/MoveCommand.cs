using UnityEngine;
using System.Collections;

public class MoveCommand : MonoBehaviour {

    //private Unit selectedUnit;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CommandMove ()
    {
        bool isCommanding = !ClickManager.instance.isCommanding;
        if (isCommanding)
        {
            ClickManager.instance.isCommanding = true;
            Debug.Log("Command");
            //selectedUnit = ClickManager.instance.selectedUnit.GetComponent<Unit>();
            ClickManager.instance.selectedUnit.GetComponent<Unit>().Move();
        }
        else
        {
            ClickManager.instance.isCommanding = false;
            Debug.Log("unCommand");
        }
    }
}
