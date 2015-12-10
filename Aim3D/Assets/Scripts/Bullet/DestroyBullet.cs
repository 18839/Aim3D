using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {
    //Vector3
    private Vector3 _myRotation;
    //Vector3


    //GameObject
    private GameObject _getPlayerRotation;
    //GameObject

    void Start()
    {
        _getPlayerRotation = GameObject.FindGameObjectWithTag("Player");
        _myRotation = _getPlayerRotation.transform.TransformDirection(Vector3.back);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "ReflectWall")
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(_myRotation * 100, ForceMode.Impulse);
        }
    }
}
