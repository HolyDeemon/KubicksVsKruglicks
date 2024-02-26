using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public FPS_Camera camera;
    public PlayerMover Player;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraNOYaxis = camera.gameObject.transform.forward;
        cameraNOYaxis.y = 0;
        transform.LookAt(transform.position + cameraNOYaxis);
        transform.Rotate(Vector3.up, 90f);
        animator.SetFloat("Moving", Player.TotalMovingSpeed);
        animator.SetFloat("X", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Y", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("horHeadRotation", 0);
        animator.SetFloat("verHeadRotation", -camera.rotX);

    }



}
