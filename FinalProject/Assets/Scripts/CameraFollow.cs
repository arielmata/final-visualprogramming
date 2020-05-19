using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    private Vector3 smoothPos;
    public float smoothSpeed = 0.015f;

    public GameObject cameraBottomBorder;
    public GameObject cameraTopBorder;

    private float cameraHalfHeight;

    // Start is called before the first frame update
    void Start()
    {
        cameraHalfHeight = Camera.main.orthographicSize * 1/Camera.main.aspect;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float borderBottom = cameraBottomBorder.transform.position.y + 2*cameraHalfHeight;
        float borderTop = cameraTopBorder.transform.position.y - cameraHalfHeight;

        smoothPos = Vector3.Lerp(this.transform.position,
            new Vector3(this.transform.position.x,
            Mathf.Clamp(followTransform.position.y, borderBottom, borderTop),
            this.transform.position.z), smoothSpeed);

        this.transform.position = smoothPos;
    }
}
