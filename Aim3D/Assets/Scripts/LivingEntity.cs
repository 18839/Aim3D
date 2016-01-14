using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;//how much health at the start
    protected float health;//how much health
    protected bool dead;//to be or not to be :)

    public event System.Action OnDeath;

    [SerializeField]
    private GameObject _healthPickup;

    protected virtual void Start()
    {
        health = startingHealth;//sets health
    }

    public void TakeHit(float damage, RaycastHit hit)//when hit take damg and possebly die
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            death();
        }
    }
    public void PlayerDamg(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Debug.Log("killed by minnion");
        }
    }

    protected void death()//when would this be used >_>
    {
        dead = true;
        if (OnDeath != null)
            OnDeath();
        Instantiate(_healthPickup, gameObject.transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }
}