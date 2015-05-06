using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseOverSelection : MonoBehaviour {

    public SelectableUnit selectedUnit;
    public Text selectionText;
    public GameObject selectionCube;

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
            selectedUnit = collided.GetComponent<SelectableUnit>();
            selectionCube.transform.SetParent(collided, false);
            selectionCube.SetActive(true);
            PathingUnit child = selectedUnit.GetComponent<PathingUnit>();
            if (null != child)
            {
                selectedUnit.canMove = true;
                selectedUnit = child;

            }
            else
            {
                selectedUnit.canMove = true;
            }
        }
        else
        {
            selectedUnit = null;
            selectionCube.SetActive(false);
            selectionCube.transform.SetParent(null, false);
            selectionCube.transform.parent = null;
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
