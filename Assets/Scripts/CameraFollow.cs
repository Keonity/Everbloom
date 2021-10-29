using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    private Vector3 smoothPos;
    private float smoothSpeed = 0.5f;

    public GameObject cameraLeftBorder;
    public GameObject cameraRightBorder;
    public GameObject cameraUpBorder;
    public GameObject cameraDownBorder;

    private float cameraHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        float borderLeft = cameraLeftBorder.transform.position.x + cameraHalfWidth;
        float borderRight = cameraRightBorder.transform.position.x - cameraHalfWidth;
        float borderDown = cameraDownBorder.transform.position.y + cameraHalfWidth;
        float borderUp = cameraUpBorder.transform.position.y - cameraHalfWidth;

        smoothPos = Vector3.Lerp(this.transform.position,
            new Vector3(Mathf.Clamp(followTransform.position.x, borderLeft, borderRight),
           Mathf.Clamp(followTransform.position.y, 0f, 4.7f),
            this.transform.position.z), smoothSpeed); ;

        this.transform.position = smoothPos;
    }
}
