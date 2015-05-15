using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static InputManager inputManager;
    public static Pathfinding pathfinding;
    public static PathRequestManager pathRequestManager;
    public static UIManager uiManager;
    public static UnitCommand unitCommand;

    void Awake ()
    {
        if (null == gameManager)
            gameManager = this;
        else if (this != gameManager)
        {
            Destroy(this);
        }

        inputManager = GetComponent<InputManager>();
        pathfinding = GetComponent<Pathfinding>();
        pathRequestManager = GetComponent<PathRequestManager>();
        uiManager = GetComponent<UIManager>();
        unitCommand = GetComponent<UnitCommand>();


        Debug.Log("GameManager ready");
    }
}