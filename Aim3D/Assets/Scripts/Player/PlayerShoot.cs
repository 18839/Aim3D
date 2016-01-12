using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField]
    private GameObject _bulletToShoot;
    
    private bool _shooting = false;


    void Update()
    {

        

        if (Input.GetButtonDown("Fire1"))
        {
            _shooting = true;
            Instantiate(_bulletToShoot, transform.position, Quaternion.identity);

        }
        else
            _shooting = false;
    }
}