using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseOverSelection : MonoBehaviour {

    public Unit selectedUnit;
    public Text selectionText;
    public Transform selectionCube;

    //public float cooldown = 1f;
    //public float timer = 0f;

    public LayerMask selectionMask;

    public Vector3 mousePosition;
    //public Vector3 startSelection;
    //public Vector3 endSelection;

	// Use this for initialization
	void Start () {
        //selectedUnit = new GameObject();
	}
	
	// Update is called once per frame
    void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("click");
            Click();
        }
    }

    void Click ()
    {
        mousePosition = Input.mousePosition;

        Debug.Log("Casting Ray");
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectionMask))
        {
            Collider collided = hit.collider;
            selectedUnit = collided.GetComponent<Unit>();
            selectionCube.position = collided.transform.position;
        }
        else
        {
            selectedUnit = null;
        }
        UpdateSelectionUI();
    }

    void UpdateSelectionUI ()
    {
        if (null != selectedUnit)
        {
            selectionText.text = "Selected: " + selectedUnit.name;
        }
        else
        {
            selectionText.text = "Selected: Nothing";
        }
    }
}
