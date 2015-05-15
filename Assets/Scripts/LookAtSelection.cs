using UnityEngine;
using System.Collections;

public class LookAtSelection : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float factor = 2f;


    
    void Update ()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        if (transform.rotation != targetRotation)
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, factor * Time.deltaTime);
    }
}
