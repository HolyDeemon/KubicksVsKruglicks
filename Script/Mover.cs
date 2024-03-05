using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mover : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float Drag;
    private float sprintmultiplier = 1f;
    public float SprintMultiplier = 1.25f;
    public bool IsSprinting;
    public float TotalMovingSpeed;

    [Header("Jumping")]
    public float jupmForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    private float horInput;
    private float verInput;

    private Vector3 moveDirection;
    public Weapon gun;
    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    public void MoverInput(Vector2 input, bool sprint, bool jump)
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround); //проверка нахождени€ игрока на земле с помощью Raycast.
        SpeedControl();//функци€ контролирующа€ скорость игрока.

        if (grounded) { rb.drag = Drag; } //проверка, позвол€юща€ примен€ть Drag только к наход€щимс€ на земле.
        else { rb.drag = 0; }

        horInput = input.x;
        verInput = input.y;

        IsSprinting = sprint && verInput > 0; //проверка, позвол€юща€ бежать только вперЄд.

        if (IsSprinting && !gun.IsAiming) //проверка, не позвол€юща€ бежать при прицеливании и отвечающа€ за корректное отображение анимации оружи€ при беге.
        {
            sprintmultiplier = SprintMultiplier;
            gun.IsSprinting = true;
        }
        else
        {
            sprintmultiplier = 1f;
            gun.IsSprinting = false;
        }

        if (jump && readyToJump && grounded) //проверка, исполн€ема€ при нажатии кнопки прыжка, вызывающа€ функцию отвечающую за прыжок и работает с откатом прыжка.
        {
            readyToJump = false;

            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    public void MoveObject()
    {
        moveDirection = orientation.forward * verInput + orientation.right * horInput; //вычисление направлени€ движени€.

        var totalSpeed = moveSpeed * sprintmultiplier; //домножение скорости на умножитель бега, равный нулю при не нажатой кнопке бега.

        rb.AddForce(moveDirection * totalSpeed * 10f, ForceMode.Force); //примен€ем силу к Rigidbody дл€ перемещени€ объекта в заданном направлении.
        if (grounded) { rb.AddForce(moveDirection * totalSpeed * 10f, ForceMode.Force); } //проверка, добавл€юща€ множитель нахождени€ в воздухе только когда движущийс€ находитс€ в воздухе.
        else if (!grounded) { rb.AddForce(moveDirection * totalSpeed * 10f * airMultiplier, ForceMode.Force); }

        TotalMovingSpeed = rb.velocity.magnitude / moveSpeed; //вычисл€ем относительную скорость объекта делением вектора скорости на базовую скорость.
    }

    private void SpeedControl()
    {
        var totalSpeed = moveSpeed * sprintmultiplier;//вычисл€ем общую скорость объекта.

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //создаем новый вектор flatVel, содержащий только горизонтальные компоненты скорости объекта.

        if (flatVel.magnitude > moveSpeed) //проверка, не дающа€ скорости объекта превысить лимит.
        {
            Vector3 limitedVel = flatVel.normalized * totalSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);//при вызове функции обнул€ем вертикальную скорость объекта, чтобы избежать накоплени€ вертикальной скорости.

        rb.AddForce(transform.up * jupmForce, ForceMode.Impulse);//примен€ем силу вверх по направлению объекта.
    }
    private void ResetJump() //небольша€ функци€, переключающа€ флаг позвол€ющий прыгать.
    {
        readyToJump = true;
    }
}
