using UnityEngine;
using System.Collections;

public class HealPickUp : MonoBehaviour {

    [SerializeField]
    private float _damage = -1;//the ammount of damg this thing does

	void Update () 
    {
      
	}

    private void OnTriggerEnter(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();//check for component idamagable on the hit object
        if (damageableObject != null)//"if object has idamagable"
        {
            damageableObject.TakeHit(_damage, hit);//damage it
        }
        Debug.Log(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);//destroy this object(the projectile)
    }
}
