using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    
    public static InputManager inputManager;
    public static TaskManager taskManager;
    public static UIManager uiManager;
    
    //public static Grid grid;
    //public static Pathfinding pathfinding;
    //public static PathRequestManager pathRequestManager;
    //public static UnitCommand unitCommand;
    
    void Awake ()
    {
        if (null == gameManager)
            gameManager = this;
        else if (this != gameManager)
        {
            Destroy(this);
        }

        inputManager = GetComponent<InputManager>();
        taskManager = GetComponent<TaskManager>();
        uiManager = GetComponent<UIManager>();
        
        //grid = GetComponent<Grid>();
        //pathfinding = GetComponent<Pathfinding>();
        //pathRequestManager = GetComponent<PathRequestManager>();
        //unitCommand = GetComponent<UnitCommand>();
        

        Debug.Log("GameManager ready");
    }

    public static void IfSelectedUpdateUI (SelectableObject that)
    {
        if (that == inputManager.selectedUnit)
            uiManager.UpdateUI(that);
    }
}