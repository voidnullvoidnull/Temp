using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System;

[RequireComponent(typeof(LineRender))]
public class CameraRotator : MonoBehaviour{

    public float speed = 5;
    public float smoothSpeed = 10;
    public Transform pivot;
    LineRender lineRender;

    float maxVertAngle = 70;

    float angleHor = 0;
    float angleVert = 0;
    float distance = 20;
    Vector3 pos = new Vector3();
    Quaternion rotation = new Quaternion();

    void Start()
    {
        lineRender = GetComponent<LineRender>();
    }

	void Update ()
    {

        distance -= Input.GetAxis("Mouse ScrollWheel") * speed;
        distance = Mathf.Clamp(distance, 5, 500);


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                lineRender.SetTarget(hit.transform);
                SetPivot(hit.point);
            }
        }
        if (Input.GetMouseButton(0))
        {
            angleHor -= Input.GetAxis("Mouse X") * speed;
            angleVert += Input.GetAxis("Mouse Y") * speed;
            angleVert = Mathf.Clamp(angleVert, -maxVertAngle, maxVertAngle);
        }

    }

    void LateUpdate()
    {
        transform.localPosition = new Vector3(0, 0, -distance);
        rotation = Quaternion.Euler(angleVert, angleHor, 0);
        pivot.rotation = Quaternion.Lerp(pivot.rotation, rotation, Time.deltaTime * smoothSpeed);
        pivot.position = Vector3.Lerp(pivot.position, pos, Time.deltaTime * smoothSpeed);
    }

    public void SetPivot(Vector3 point)
    {
        pos = point;
    }
}
