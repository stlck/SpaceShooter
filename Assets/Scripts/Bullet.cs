using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Weapon Owner;
	public float Damage = .25f;
	public float Speed;
	public bool Aoe = false;
	public float Range = 3;

	void Update()
	{
		transform.position += transform.right * Time.deltaTime * Speed;
	}

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D other)
	//void OnTriggerEnter2D(Collider2D other) 
	{
		/*if (other.gameObject.tag == "Hittable") 
		{
			Weapon.Instance.WeaponEffects.transform.position = transform.position;
			Weapon.Instance.WeaponEffects.Play();

			if(!Aoe)
			{

				other.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
			}
			else
			{
				var coll = Physics2D.OverlapCircleAll(transform.position, Range);

				foreach(var c in coll)
					if(c.gameObject.tag == "Hittable")
					{
						var e = c.gameObject.GetComponent<Enemy>();
						var dist = Vector3.Distance(transform.position, c.transform.position) * .1f;
						e.StartCoroutine(e.sendDamage( dist, Damage));
					}
			}
			Destroy (gameObject);
		}*/
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Hittable") 
		{
			Owner.WeaponEffects.transform.position = transform.position;
			Owner.WeaponEffects.Play();
			
			if(!Aoe)
			{
				
				other.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
			}
			else
			{
				var coll = Physics2D.OverlapCircleAll(transform.position, Range);
				
				foreach(var c in coll)
					if(c.gameObject.tag == "Hittable")
				{
					var e = c.gameObject.GetComponent<Enemy>();
					var dist = Vector3.Distance(transform.position, c.transform.position) * .1f;
					e.StartCoroutine(e.sendDamage( dist, Damage));
				}
			}
			Destroy (gameObject);
		}
	}
}
