using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControler : MonoBehaviour
{
    float sensitivity = 100f;  // float to controle the sensativity of the camera 
    public Transform body; 
    float xRotation = 0f; // float to set the default rotation of the camera

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor when the game starts
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; // retrive the value of how much we have moved the x and y valuse 
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime; // using untiys built in system then multiplys them by out sensityity for control independant of framerate 

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // limits the value of the Xrotation varaible

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // transfroms the rotating of the x axis by the xRotation varaible
        body.Rotate(Vector3.up * mouseX); // rotates the body transfrom around the y axis by the value of mouse x
    }
}
