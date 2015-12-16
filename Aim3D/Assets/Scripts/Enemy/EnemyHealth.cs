using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private float _enemyHealth = 500f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (_enemyHealth <= 0)
    {
        Destroy(this.gameObject);
    }
	}

    void OnCollisionEnter(Collision bullet)
    {
        if (bullet.gameObject.name == "BulletTest")
        {
            _enemyHealth -= 100;
        }
    }
        
}
