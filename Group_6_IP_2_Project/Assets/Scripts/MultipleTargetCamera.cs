using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    // Grabs the MenuManager Script 
    public MenuManager menuMana;

    // makes a list for the player characters 
    public List<Transform> target;

    public Vector3 offset;

    public Vector3 velocity;

    // grabs camera Game object 
    private Camera cam;

    public float smoothTime = .5f;
    public float minZoom = 125f;
    public float maxZoom = 15f;
    public float zoomLimiter = 75f;
    public float zoomLimitY = 10f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target.Count == 0) // if their are no characters to target 
            return; // stops the other functions until more than 0 targets are avalible 

        Move();

        Zoom();
    }

    private void Zoom() // This method sets the zoom for the camera 
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter); // these lines of code work by grabbing the max and min zoom levels 
        float newZoomY = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistanceYAxis() / zoomLimitY); // and applying them with the distance between the targets devided by the limiters to keep the zoom in bounds of the level
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime); // these control the FOV of the camera by useing the newwly created new zoom varibales 
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoomY, Time.deltaTime); // and applies Time delta time to keep the constant changes from refreshing to slow or to fast.
    }

    private void Move() // this method moves the camera to keep it on the centre point 
    {
        Vector3 centerPoint = GetCenterPoint(); // assigns centre point by grabbing the get centre point function

        Vector3 newPosition = centerPoint + offset; // this allows the camera to be moved from the centre to make an appropriate angle to play the game

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime); // slows the camera so its not snapping to the centre 
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(target[0].position, Vector3.zero); // bounds the targets on the x axis meaning they will be kept on screen
        for (int i = 0; i < target.Count; i++)
        {
            bounds.Encapsulate(target[i].position); // finds the targets position 
        }

        return bounds.size.x; // bounds along the axis 
    }

    private float GetGreatestDistanceYAxis() // this code is the same as the code above but is allined along the Y axis instead of the X axis 
    {
        var bounds = new Bounds(target[0].position, Vector3.zero);
        for (int i = 0; i < target.Count; i++)
        {
            bounds.Encapsulate(target[i].position);
        }

        return bounds.size.y;
    }

    private Vector3 GetCenterPoint() // used to find the distance between the targets to find the centre point 
    {
        if (target.Count == 1) // checks to see if their is only one target 
        {
            return target[0].position; // if their is then it will keep the camera on the targets position 
        }

        var bounds = new Bounds(target[0].position, Vector3.zero); // if their are more than one target 
        for (int i = 0; i < target.Count; i++)
        {
            bounds.Encapsulate(target[i].position); // it will find the centre position between each target by using bounds to contain these targets distance.
        }

        return bounds.center;
    }
}
