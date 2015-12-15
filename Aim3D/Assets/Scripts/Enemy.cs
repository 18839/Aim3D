using UnityEngine;
using System.Collections;

public class Enemy : LivingEntity//imports the living entity script
{
    Transform player; //who would this be o.o

    [SerializeField]
    private float _attackRange = 1.5f;//the range of his attack...
    [SerializeField]
    private float _attackspeed = 1;//how fast it will attack
    private float _nextAttack;//how much time til it can attack again

    protected override void Start()
    {
        base.Start();//gets the start from living entity
        player = GameObject.FindGameObjectWithTag("Player").transform;//player is the thing with the tag player(what a suprise)
    }

    // Update is called once per frame
    void Update () {
        if (Time.time > _nextAttack)//is it time to attack?
        {
            float sqrDistance = (player.position - transform.position).sqrMagnitude;
            if (sqrDistance < Mathf.Pow(_attackRange, 2))
            {
                _nextAttack = Time.time + _attackspeed;
                print("pow");
            }
        }
	}
    //IEnumerator attack
}
