using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitter : MonoBehaviour
{
    public float desiredAspectWidth = 16f;
    public float desiredAspectHeight = 9f;
    float desiredAspect;
    float initialCameraSize;
    Camera cam;

    private void Awake()
    {
        desiredAspect = desiredAspectWidth / desiredAspectHeight;
        cam = this.GetComponent<Camera>();
        initialCameraSize = cam.orthographicSize;
    }

    void Update()
    {
        //Debug.Log(aspect);
        cam.orthographicSize = initialCameraSize * desiredAspect / cam.aspect;
    }
}
