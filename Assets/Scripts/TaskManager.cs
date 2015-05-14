using UnityEngine;
using System.Collections;

public class TaskManager : MonoBehaviour {
    
    public static TaskManager instance = null;
    public GameObject canvas;

    public Transform objectHolder;
    public GameObject placeholder;
    
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

        canvas.SetActive(true);

        objectHolder = new GameObject("GameObjects").transform;

        
	}

    public void MakePlaceholder (Vector3 location)
    {
        GameObject instance = Instantiate(placeholder, location, Quaternion.identity) as GameObject;
        instance.transform.parent = objectHolder;
    }

}
