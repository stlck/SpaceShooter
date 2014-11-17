using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public Transform HealthBar;
	public float Health = 2;
	public Transform DeathEffect;
	public static PlayerHealth Instance;

	float startHealth;

	// Use this for initialization
	void Start () {
		Instance = this;
		startHealth = Health;
	}

	void Update()
	{
		if(Health <= 0)
		{
			if(DeathEffect != null)
				Instantiate(DeathEffect, transform.position, DeathEffect.rotation);
			
			Manager.EndGame();
			
			//Weapon.Instance.gameObject.SetActive(false);
		}

		HealthBar.localScale = new Vector3 (Health / startHealth, 1, 1);
	}
	
	// Update is called once per frame
	void OnCollisionStay2D(Collision2D coll) {

		
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (Health <= 0)
			return;
		
		if (coll.gameObject.tag == "Hittable") {
			Health -= Time.deltaTime;
		}
	}
}
