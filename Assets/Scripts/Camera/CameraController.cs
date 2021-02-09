using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Follow
    public Transform target;
    private Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 0.1f;

    //Zoom
    private Camera cam;
    private float targetZoom;
    [SerializeField] private float zoomFactor = 3f;
    [SerializeField] private float zoomLerpSpeed = 10;

    //Zoom Tutorial
    //https://www.youtube.com/watch?v=jmTUUP33GHs
    //How to zoom camera in Unity - [Unity Tutorial]
    //Tutorial by Danny Bergs

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }



    void FixedUpdate()
    {
        FollowTarget();
        CheckZoom();
    }

    void FollowTarget()
    {
        if (target != null)
        {
        //transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, -10), 0.1f);
        transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
        }
    }

    void CheckZoom()
    {
        float scrollData;
        scrollData = Input.GetAxisRaw("Mouse ScrollWheel");
        //Debug.Log(scrollData);

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 4.5f, 8f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}
