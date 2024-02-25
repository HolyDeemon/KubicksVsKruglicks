using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFly : MonoBehaviour
{
    [SerializeField] private float translationSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;

    void Start()
    {

    }

    Vector3 lastPosition, curPosition;
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            //Camera Translation
            this.transform.Translate(GetBaseInput() * translationSpeed * Time.deltaTime, Space.Self);

            //Camera Rotation
            this.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * rotationSpeed * Time.deltaTime, Space.World);
            this.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * rotationSpeed * Time.deltaTime, Space.Self);
        }

    }

    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.UpArrow)) p_Velocity += new Vector3(0, 0, 1);

        if (Input.GetKey(KeyCode.DownArrow)) p_Velocity += new Vector3(0, 0, -1);

        if (Input.GetKey(KeyCode.LeftArrow)) p_Velocity += new Vector3(-1, 0, 0);

        if (Input.GetKey(KeyCode.RightArrow)) p_Velocity += new Vector3(1, 0, 0);

        if (Input.GetKey(KeyCode.W)) p_Velocity += new Vector3(0, 1, 0);

        if (Input.GetKey(KeyCode.S)) p_Velocity += new Vector3(0, -1, 0);

        return p_Velocity;
    }

}
