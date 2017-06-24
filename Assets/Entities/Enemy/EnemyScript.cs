using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float Hp = 15;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void HitByProjectile(ProjectileScript projectile)
    {
        Debug.Log("trigger, enemy hit!");
        var damage = projectile.GetDamage();
        Hp = Hp - damage;
        Debug.Log(Hp);
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("not trigger!");
    }

    void OnTriggerEnter2D(Collider2D collider) {
        var projectile = collider.gameObject.GetComponent<ProjectileScript>();
        if (projectile)
        {
            this.HitByProjectile(projectile);
        }
    }
}
