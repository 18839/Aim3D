using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //Floats
    [SerializeField]
    private float _speed = 6.0F;
    private float _jumpSpeed = 8.0F;
    private float _gravity = 20.0F;
    private float _sprintTimer = 5f;
    //Floats

    //Vector3
    private Vector3 _moveDirection = Vector3.up;
    //Vector3
    
    //CharacterController
    private CharacterController _characterController;
    //CharacterController

    //Bool
    private bool _tiredPlayer = false;
    //Bool


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void PlayerMoving()
    {
        if (_characterController.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= _speed;

            if (Input.GetButton("Jump"))
                _moveDirection.y = _jumpSpeed;


            
        }

        _moveDirection.y -= _gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    void PlayerSprint()
    {
        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           
            print(_speed);
            _speed = 12f;

            while (_sprintTimer >= 0)
                _sprintTimer -= 0.1f;

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = 6f;
            while (_sprintTimer <= 5)
            _sprintTimer+= 0.1f;
        }

        if (_sprintTimer <= 0.1f)
            _tiredPlayer = true;
        else
            _tiredPlayer = false;
    }


    void Update()
    {
        PlayerMoving();
        PlayerSprint();     
    }
}