using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private LayerMask _collisionMask;//layer wich the projectile checks for

    [SerializeField]
    private float _projectileSpeed = 10;//the speed of the projectile >_>
    [SerializeField]
    private float _damage = 1;

    public void SetSpeed(float newSpeed)//here otherclasses can change the speed of the projectile(incase of more guns or something like that)
    {
        _projectileSpeed = newSpeed;
    }

	void Update () {
        float moveDistance = _projectileSpeed * Time.deltaTime;//this calculates the distance it moves before actualy moving
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);//this moves the projectile forward
	}

    void CheckCollisions(float moveDistance)//checks if the projectile hits something before hitting it
    {
        Ray ray = new Ray(transform.position, transform.forward);//defines a ray that gets a starting pos and a direction
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, moveDistance, _collisionMask, QueryTriggerInteraction.Collide))//actual raycast, queryTriggerInteraction allows me to decide if i want it to collide with triggers to, wich is what i want in this case
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)//this wil take in information abbout the object hit
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();//check for component idamagable on the hit object
        if(damageableObject != null)//"if object has idamagable"
        {
            damageableObject.TakeHit(_damage, hit);//damage it
        }
        Debug.Log(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);//destroy this object(the projectile)
    }
}