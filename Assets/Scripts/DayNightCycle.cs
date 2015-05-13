using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

    public float degPerSec;
    
	// Use this for initialization
	void Start () {
        transform.localEulerAngles = new Vector3(70f, 90f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(degPerSec * Time.deltaTime, 0f, 0f);
	}
}
