using UnityEngine;
using System.Collections;

public class GunAnimations : MonoBehaviour {

    [SerializeField]
    private GameObject _mainCamera;

    [SerializeField]
    private float _maxCamZoom = 30f;

    [SerializeField]
    private float _zoomSpeed = 5f;

    private Camera _cameraFOV;

    private bool _cameraZoomIn = false;

    Animator anim;

    Animator camAnim;

    void Start()
    {
        anim = GetComponent<Animator>();
        camAnim = _mainCamera.GetComponent<Animator>();
       _cameraFOV = _mainCamera.GetComponent<Camera>();
    }

	void Update () 
    {
        Zooming();
	if (Input.GetKeyDown(KeyCode.Mouse1))
    {
        _cameraZoomIn = true;
        anim.SetBool("FocusGun", true);
        anim.SetBool("ReturnGun", false);
        anim.SetBool("NeutralGun", false);
        anim.SetBool("SprintGun", false);
    }

    if (Input.GetKeyUp(KeyCode.Mouse1))
    {
        _cameraZoomIn = false;
        anim.SetBool("FocusGun", false);
        anim.SetBool("ReturnGun", true);
        
      anim.SetBool("NeutralGun", true);
    }

    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
        anim.SetBool("SprintGun", true);
        anim.SetBool("FocusGun", false);
        anim.SetBool("NeutralGun", false);
        anim.SetBool("ReturnGun", false);
        anim.SetBool("NeutralGun", false);
        camAnim.SetBool("NeutralCam", false);
        camAnim.SetBool("SprintCam", true);
       
    }


    if (Input.GetKeyUp(KeyCode.LeftShift))
    {
        anim.SetBool("FocusGun", false);
        anim.SetBool("NeutralGun", true);
        anim.SetBool("ReturnGun", false);
        anim.SetBool("SprintGun", false);
        camAnim.SetBool("SprintCam", false);
        camAnim.SetBool("NeutralCam", true);
    } 
	}

    void Zooming()
    {
        if (_cameraZoomIn)
        {
            if (_cameraFOV.fieldOfView >= _maxCamZoom)
            {
                _cameraFOV.fieldOfView -= _zoomSpeed;
            }
        }
        else
        {
            while (_cameraFOV.fieldOfView <= 59)
            {
                _cameraFOV.fieldOfView+= 0.1f;
            }
        }
    }
}
