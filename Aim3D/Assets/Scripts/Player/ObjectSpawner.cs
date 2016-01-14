using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{

    //Int
    [SerializeField]
    private int _grid;
    [SerializeField]
    private Transform _wall;
    [SerializeField]
    private Transform _turret1;
    [SerializeField]
    private Transform _turret2;
    private int _placeOpen;//check is this space is taken and if so, by what(0=nothing 1=wall 2-turret etc) 
    private bool wavedone = true;//haal dit nog bij de spawner
    private bool clearPath = true;//maak in pathfinding wanneer je kan lopen
    private int _recources;//the ammount of recources that you have
    [SerializeField]
    private int _wallCost;//the ammount of recources a wall wil cost
    [SerializeField]
    private int _turret1Cost;//the ammount of recources a turrettype1 wil cost
    [SerializeField]
    private int _turret2Cost;//the ammount of recources a turrettype2 wil cost
    [SerializeField]
    private int _blockChoice;//what you pick, a wall(1) or turret(2) or turrettype2(3) etc
    private Vector3 _gridPos;
    //Int

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (wavedone)
        {
            var pos = new Vector3(Screen.width / 2, Screen.height / 2, 10);//get midle x, midle y, how far it can go
            var position = Camera.main.ScreenToWorldPoint(pos);//use the value's from above actualy in the view of the camera and not the world positions
            _gridPos = new Vector3(Mathf.Round(position.x / _grid) * _grid, 0f, Mathf.Round(position.z / _grid) * _grid);//here we round the position so you get a grid effect
            transform.position = _gridPos;//here we actualy place it
            if (clearPath)
            {
                if (_recources >= _wallCost && _blockChoice == 1 && _placeOpen == 0)
                {
                    if(Input.GetButtonDown("Fire1"))
                    {
                        Instantiate(_wall, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        _recources =- _wallCost;
                    }
                }
                if (_recources >= _turret1Cost && _blockChoice == 2 && _placeOpen == 1)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {

                        _recources = -_turret1Cost;
                    }
                }
                if (_recources >= _turret2Cost && _blockChoice == 3 && _placeOpen == 1)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {

                        _recources = -_turret2Cost;
                    }
                }
            }
        }
        else
        {
            transform.position = new Vector3(100, 0, 0);//where we place it out of the screen
        }
        
    }
}
