using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //Floats
    private float _speed = 6.0F;
    private float _jumpSpeed = 8.0F;
    public float _gravity = 20.0F;
    //Floats

    private Vector3 moveDirection = Vector3.up;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= _speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = _jumpSpeed;

        }


        moveDirection.y -= _gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}