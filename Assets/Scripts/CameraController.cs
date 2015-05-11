using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private Transform cameraTilt;
    private Transform cameraZoom;

    //[SerializeField]
    //private float rotateSpeed = 1f;
    [SerializeField]
    private float mouseSlideSpeed = 1f;
    //[SerializeField]
    //private float keyboardSlideSpeed = 1f;
    [SerializeField]
    private float zoomSpeed = 5f;
    [SerializeField]
    private float panSpeed = 1f;

    [SerializeField]
    private float zoomTarget = -40f;
    private float zoomCurent;

    // Use this for initialization
    void Awake ()
    {
        if (null == cameraTilt)
        cameraTilt = transform.GetChild(0);
        if (null == cameraZoom)
        cameraZoom = cameraTilt.GetChild(0);

        zoomCurent = cameraZoom.localPosition.z;
    }

    void Update ()
    {
        if (Input.GetButton("Fire3"))
        {
            transform.Translate(-1.0f * panSpeed * mouseSlideSpeed * new Vector3(Input.GetAxis("Mouse X"), 0.0f, Input.GetAxis("Mouse Y")));
        }

        zoomCurent = cameraZoom.localPosition.z;
        zoomTarget += 50f * Input.GetAxis("Mouse ScrollWheel");
        zoomTarget = Mathf.Clamp(zoomTarget, -110f, -10f);

        if (Mathf.Abs(zoomTarget - zoomCurent) > 0.05f)
            cameraZoom.localPosition = Vector3.forward * Mathf.Lerp(zoomCurent, zoomTarget, zoomSpeed * Time.deltaTime);
    }
}
