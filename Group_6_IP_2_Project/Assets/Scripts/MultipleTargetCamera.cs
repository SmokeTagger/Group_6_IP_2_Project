using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public MenuManager menuMana;

    public List<Transform> target;

    public Vector3 offset;

    public Vector3 velocity;

    private Camera cam;

    public float smoothTime = .5f;
    public float minZoom = 125f;
    public float maxZoom = 15f;
    public float zoomLimiter = 75f;
    public float zoomAug = 10f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target.Count == 0)
            return;

        Move();

        Zoom();
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistanceXAxis() / zoomLimiter);
        float newZoomY = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistanceYAxis() / zoomAug);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoomY, Time.deltaTime);
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private float GetGreatestDistanceXAxis()
    {
        var bounds = new Bounds(target[0].position, Vector3.zero);
        for (int i = 0; i < target.Count; i++)
        {
            bounds.Encapsulate(target[i].position);
        }

        return bounds.size.x;
    }

    private float GetGreatestDistanceYAxis()
    {
        var bounds = new Bounds(target[0].position, Vector3.zero);
        for (int i = 0; i < target.Count; i++)
        {
            bounds.Encapsulate(target[i].position);
        }

        return bounds.size.y;
    }

    private Vector3 GetCenterPoint()
    {
        if (target.Count == 1)
        {
            return target[0].position;
        }

        var bounds = new Bounds(target[0].position, Vector3.zero);
        for (int i = 0; i < target.Count; i++)
        {
            bounds.Encapsulate(target[i].position);
        }

        return bounds.center;
    }
}
