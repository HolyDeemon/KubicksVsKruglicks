using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPS_Camera : MonoBehaviour
{
    public PlayerMover Player;

    public float sensX;
    public float sensY;
    
    public float rotX;
    public float rotY;

    public Transform orientation;
    public float kickback = 0f;

    public float FOV = 75f;
    public float SprintFov;

    public KeyCode escapeButton = KeyCode.Escape;

    private void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(escapeButton))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu");
        }


        float sprintfov = 0;
        if (Player.IsSprinting)
        {
            sprintfov = SprintFov;
        }


        kickback = Mathf.Lerp(kickback, 0f, 0.05f);
        
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotY += mouseX;
        rotX -= mouseY;

        rotX = Mathf.Clamp(rotX, -80f, 80f);

        transform.rotation = Quaternion.Euler(rotX - kickback, rotY, 0);
        
        orientation.rotation = Quaternion.Euler(0, rotY, 0);
        orientation.transform.position = Vector3.up * rotX;


        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, FOV + sprintfov, 0.01f);
    }

}