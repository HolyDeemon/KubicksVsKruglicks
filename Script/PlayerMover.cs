using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMover : Mover
{
    [Header("Movement")]
    public KeyCode SprintKey = KeyCode.LeftShift;

    [Header("Jumping")]
    public KeyCode jumpKey = KeyCode.Space;

    private void Update()
    {
        MoverInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), Input.GetKey(SprintKey), Input.GetKey(jumpKey));
    }

    private void FixedUpdate()
    {
        MoveObject();
    }
}
