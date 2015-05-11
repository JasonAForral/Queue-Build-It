using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickManager : MonoBehaviour {

    public GameObject selectionCube;
    public Text selectionText;
    public LayerMask selectionMask;
    
    //[SerializeField]
    public GameObject selectedUnit;

    [SerializeField]
    private GameObject guiUnit;
    [SerializeField]
    private GameObject guiStructure;

    public bool isCommanding;

    
    private Vector3 mousePosition;

    private Vector3 clickDestination;

    public static ClickManager instance;
    
    void Awake () {
        if (null == instance)
            instance = this;
        else if (this != instance)
            Destroy(gameObject);
    }

    void Start ()
    {
        ResetUI();
    }
    void Update ()
    {
        if (!isCommanding && Input.GetButtonDown("Fire1"))
        {
            Click();
        }
    }

    void Click ()
    {
        mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectionMask))
        {
            Transform collided = hit.collider.transform;
            selectedUnit = collided.gameObject;
            selectionCube.transform.SetParent(collided, false);
            Vector3 selection = selectionCube.transform.position;
            selectionCube.transform.position = new Vector3(selection.x, 1f, selection.z);
            selectionCube.SetActive(true);
            
            ISelectable doStuff = selectedUnit.GetComponent<ISelectable>();
            doStuff.Select();
            
            ResetUI();

            doStuff.DisplayUI();
        }
        else
        {
            //selectedGameObject = null;
            //selectionCube.SetActive(false);
            //selectionCube.transform.SetParent(null, false);
            //selectionCube.transform.parent = null;

            //ResetUI();
        }
        //UpdateSelectionUI();
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

    void ResetUI ()
    {
        guiUnit.SetActive(false);
        guiStructure.SetActive(false);
    }
}
