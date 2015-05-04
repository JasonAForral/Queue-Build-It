using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    //private Transform cameraTilt;
    //private Transform cameraZoom;

    public float rotateSpeed = 1f;
    public float mouseSlideSpeed = 1f;
    public float keyboardSlideSpeed = 1f;
    public float zoomSpeed = 50f;
    public float panSpeed = 1f;

    // Use this for initialization
    void Awake ()
    {
        //if (null == cameraTilt)
        //cameraTilt = transform.GetChild(0);
        //if (null == cameraZoom)
        //cameraZoom = cameraTilt.GetChild(0);    
    }
	
	void LateUpdate () {
        if (Input.GetButton("Fire1"))
        {
            transform.Translate(-1.0f * panSpeed * mouseSlideSpeed * new Vector3(Input.GetAxis("Mouse X"), 0.0f, Input.GetAxis("Mouse Y")));
        }
	}
}
