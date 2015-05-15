using UnityEngine;
using System.Collections;

public class TaskManager : MonoBehaviour {
    
    public static TaskManager instance = null;
    public GameObject canvas;

    public Transform objectHolder;
    public GameObject placeholder;
    public GameObject wall;
    
    public static Transform ObjectHolder
    {
        get { return ObjectHolder; }
        set { ObjectHolder = value; }
    }


    
	void Awake () {
        if (null == instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

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
        if (target == InputManager.instance.selectedUnit.gameObject)
        {
            //InputManager.instance.selectedUnit = instance.GetComponent<Structure>();
            InputManager.instance.UpdateSelection(instance.transform);
        }
        instance.transform.parent = objectHolder;
        Destroy(target);
    }

}
