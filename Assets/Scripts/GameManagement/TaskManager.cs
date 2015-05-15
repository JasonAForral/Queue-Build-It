using UnityEngine;
using System.Collections;

public class TaskManager : MonoBehaviour {
    
    public GameObject canvas;

    public Transform objectHolder;
    public GameObject placeholder;
    public GameObject wall;

    InputManager inputManager;
    Grid grid;
    
    public static Transform ObjectHolder
    {
        get { return ObjectHolder; }
        set { ObjectHolder = value; }
    }


    
	void Awake () {
        
        inputManager = GetComponent<InputManager>();
        grid = GetComponent<Grid>();
        
        objectHolder = new GameObject("ObjectHolder").transform;
        
        
	}

    public void MakePlaceholder (Vector3 location, out GameObject instance)
    {
        instance = Instantiate(placeholder, location, Quaternion.identity) as GameObject;
        instance.transform.parent = objectHolder;
    }

    public void MakeWall (GameObject target)
    {
        GameObject instance = Instantiate(wall, target.transform.position, Quaternion.identity) as GameObject;
        if (target == inputManager.selectedUnit.gameObject)
        {
            //InputManager.instance.selectedUnit = instance.GetComponent<Structure>();
            inputManager.UpdateSelection(instance.transform);
        }
        instance.transform.parent = objectHolder;

        grid.ChangeWalkableNode(instance.transform.position);

        Destroy(target);
    }

}
