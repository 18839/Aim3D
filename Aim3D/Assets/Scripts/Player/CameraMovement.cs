using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    //Axes
    private enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    private RotationAxes axes = RotationAxes.MouseXAndY;
    //Axes

    //Floats
    [SerializeField]
    private float _sensitivityX = 10F;
    [SerializeField]
    private float _sensitivityY = 10F;

    private float _minimumX = -360F;
    private float _maximumX = 360F;

    private float _minimumY = -60F;
    private float _maximumY = 60F;

    float rotationY = 0F;

    private float _cameraSpeedFloat;
    //Floats

    //Transforms
    Transform cameraTransform;
    //Transforms


    void Start()
    {

        cameraTransform = transform;

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void Update()
    {
        LookDirection();
        MovementCamera();
    }

    private void LookDirection()
    {

        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * _sensitivityY;
            rotationY = Mathf.Clamp(rotationY, _minimumY, _maximumY);

            cameraTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }

        else if (axes == RotationAxes.MouseX)
        {
            cameraTransform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivityX, 0);
        }

        else
        {
            rotationY += Input.GetAxis("Mouse Y") * _sensitivityY;
            rotationY = Mathf.Clamp(rotationY, _minimumY, _maximumY);

            cameraTransform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }

    }

    private void MovementCamera()
    {
        //Movement direction
        if (Input.GetKey(KeyCode.W))
        {
            cameraTransform.position += cameraTransform.forward * _cameraSpeedFloat * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            cameraTransform.position += -cameraTransform.forward * _cameraSpeedFloat * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            cameraTransform.position += -cameraTransform.right * _cameraSpeedFloat * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            cameraTransform.position += cameraTransform.right * _cameraSpeedFloat * Time.deltaTime;
        }
    }
   
}