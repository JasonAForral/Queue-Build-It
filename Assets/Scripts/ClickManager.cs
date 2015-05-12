using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickManager : MonoBehaviour
{

    public static ClickManager instance;
    
    public GameObject selectedUnit;
    public ClickMode currentMode = ClickMode.Selection;
    public bool isCommanding { get { return (ClickMode.Selection != currentMode); } }
    
    [SerializeField]
    private GameObject guiUnit;
    [SerializeField]
    private GameObject guiStructure;
    [SerializeField]
    private GameObject selectionCube;


    [SerializeField]
    private LayerMask selectionMask;
    [SerializeField]
    private LayerMask spaceMask;
    [SerializeField]
    private LayerMask structureMask;
    [SerializeField]
    private LayerMask unitMask;

    private Vector3 mousePosition;
    private Vector3 clickDestination;

    private Grid grid;

    
    void Awake ()
    {
        if (null == instance)
            instance = this;
        else if (this != instance)
            Destroy(gameObject);

        grid = GetComponent<Grid>();
    }

    void Start ()
    {
        ResetUI();
    }
    void Update ()
    {


        if (Input.GetButtonDown("Fire1"))
        {
            ClickPrimary();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            ClickSecondary();
        }


        if (Input.GetButtonDown("Cancel"))
        {
            if (isCommanding)
            {
                CancelCommand();
                Debug.Log("Command Cancelled");
            }
            else
            {
                ClearSelection();
                Debug.Log("Selection Cleared");
            }


        }

    }

    void ClickPrimary ()
    {
        mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        LayerMask maskInUse;
    
        switch (currentMode)
        {
       case ClickMode.Move:
        case ClickMode.Build:
        case ClickMode.Attackmove:
            maskInUse = spaceMask;
            break;
        case ClickMode.Unbuild:
            maskInUse = structureMask;
            break;
        case ClickMode.Attack:
            maskInUse = unitMask;
            break;
        case ClickMode.Selection:
        default:
            maskInUse = selectionMask;
            break;
        
        }

        if (Physics.Raycast(ray, out hit, 60f, maskInUse))
        {
            Transform other = hit.collider.transform;
            switch (currentMode)
            {
            case ClickMode.Selection:
                selectedUnit = other.gameObject;
                selectionCube.transform.SetParent(other, false);

                Vector3 selection = selectionCube.transform.position;
                selectionCube.transform.position = new Vector3(selection.x, 1f, selection.z);

                selectionCube.SetActive(true);

                ISelectable target = selectedUnit.GetComponent<ISelectable>();
                target.Select();

                ResetUI();

                target.DisplayUI();
                break;
            case ClickMode.Move:
                MoveUnit(hit.point);
                break;
            }
        }
    }

    void ClickSecondary ()
    {
        mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (isCommanding)
        {
            CancelCommand();
        }
        else
        {
            if (null != selectedUnit && null != selectedUnit.GetComponent<Unit>())
                if (Physics.Raycast(ray, out hit, 60f, spaceMask))
                {
                    MoveUnit(hit.point);
                }
        }
    }

    void UpdateSelectionUI ()
    {
        //if (null != selectedGameObject)
        //{
        //    selectionText.text = "Selected: " + selectedGameObject.name;
        //}
        //else
        //{
        //    selectionText.text = "Selected: Nothing";
        //}
    }

    public void CancelCommand ()
    {
        currentMode = ClickMode.Selection;
    }

    void MoveUnit (Vector3 destination)
    {
        Node node = grid.WorldToNode(destination);
        
        Vector3 worldpoint = node.worldPosition;
        selectedUnit.GetComponent<Unit>().Move(worldpoint);
    }

    void ClearSelection ()
    {
        selectedUnit = null;
        selectionCube.SetActive(false);
        selectionCube.transform.SetParent(null, false);
        selectionCube.transform.parent = null;
        ResetUI();

    }

    void ResetUI ()
    {
        guiUnit.SetActive(false);
        guiStructure.SetActive(false);
    }
}

[System.Serializable]
public enum ClickMode
{
    Selection,
    Move,
    Build,
    Unbuild,
    Attackmove,
    Attack
    
}

[System.Serializable]
public enum MButton
{
    Primary,
    Secondary,
    Tertiary,
    Quaternary, 
    Quinary
}
