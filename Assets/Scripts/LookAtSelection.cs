using UnityEngine;
using System.Collections;

public class LookAtSelection : MonoBehaviour {

    public Transform target;
    [SerializeField]
    private float factor = 2f;
    public Vector3 targetEuler;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update ()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        if (transform.rotation != targetRotation)
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, factor * Time.deltaTime);
        targetEuler = targetRotation.eulerAngles;
    }
}
