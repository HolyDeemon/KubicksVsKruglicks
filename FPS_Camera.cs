using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Camera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    
    float rotX;
    float rotY;

    public Transform orientation;
    public float kickback = 0f;

    public float FOV = 75f;

    private void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        kickback = Mathf.Lerp(kickback, 0f, 0.05f);
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotY += mouseX;
        rotX -= mouseY;

        rotX = Mathf.Clamp(rotX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotX - kickback, rotY, 0);
        orientation.rotation = Quaternion.Euler(0, rotY, 0);
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, FOV, 0.01f);
    }

}