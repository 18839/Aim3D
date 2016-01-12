using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{

    //Int
    [SerializeField]
    private int grid;
    private Vector3 _gridPos;
    //Int

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        var pos = new Vector3(Screen.width / 2, Screen.height / 2, 10);//get midle x, midle y, how far it can go
        var position = Camera.main.ScreenToWorldPoint(pos);//use the value's from above actualy in the view of the camera and not the world positions
        _gridPos = new Vector3(Mathf.Round(position.x / grid) * grid, 0f, Mathf.Round(position.z / grid) * grid);//here we round the position so you get a grid effect
        transform.position = _gridPos;//here we actualy place it
    }
}
