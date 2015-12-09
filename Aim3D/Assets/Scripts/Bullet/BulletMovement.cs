using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour
{
    //Vector3
    private Vector3 _myRotation;
    //Vector3

    //GameObject
    private GameObject _getPlayerRotation;
    //GameObject

    //Floats
    [SerializeField]
    private float _bulletSpeed = 50f;
    //Floats

    //Tags
    public Tags tags;
    //Tags

    void Start()
    {
        _getPlayerRotation = GameObject.FindGameObjectWithTag("Player");
        _myRotation = _getPlayerRotation.transform.TransformDirection(Vector3.forward);
        this.gameObject.GetComponent<Rigidbody>().AddForce(_myRotation * _bulletSpeed, ForceMode.Impulse);
    }

}
