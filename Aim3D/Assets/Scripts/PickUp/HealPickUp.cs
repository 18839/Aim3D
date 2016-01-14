using UnityEngine;
using System.Collections;

public class HealPickUp : MonoBehaviour
{

    private float _addedHp = -1;//the ammount of health added(its in minus becouse it uses the damg system but reversed... programming is just like magic)
    void OnTriggerEnter(Collider other)
    {
        if (tag == "Player")
        {
            IDamageable damageableObject = other.GetComponent<IDamageable>();//check for component idamagable on the hit object
            if (damageableObject != null)//"if object has idamagable"
            {
                damageableObject.PlayerDamg(_addedHp);//heal it(add negative damg)
            }
            Debug.Log(other.gameObject.name);
            GameObject.Destroy(gameObject);//destroy this object(the projectile)
        }
    }
}