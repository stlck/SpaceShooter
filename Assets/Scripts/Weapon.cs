﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class Weapon : MonoBehaviour {

	//public static Weapon Instance;
	public float timeToHit = 0.25f;
	public ParticleSystem WeaponSpray;
	public ParticleSystem WeaponEffects;
	public float Damage = 1;
	public Bullet Bullet;
	public bool Fire = false;
	public float Range = 25;
	float timer = 0f;

	// Use this for initialization
	void Start () {
		if (Network.peerType == NetworkPeerType.Disconnected) {
			transform.parent = Movement.Instance.transform;
			transform.localPosition = Vector3.zero;

			//Instance = this;
		}
		StartCoroutine (waitAndFire ());
	}
	
	// Update is called once per frame
	void Update () {
		var hits = Physics2D.RaycastAll (transform.position, transform.right, Range);
		//Debug.DrawRay (transform.position, transform.right * 25, Color.red);
		if(hits != null)
		{
			if( hits.Any( m => m.collider.tag == "Hittable"))
				Fire = true;
			else
				Fire = false;
		}


	}

	IEnumerator waitAndFire()
	{
		while (true) 
		{
			yield return new WaitForEndOfFrame();
			timer += Time.deltaTime;
			if(Fire && timer >= timeToHit)
			{
				timer = 0f;
				if(Bullet != null)
					FireWeapon();
				else
					UseSpray();
			}

			if( Bullet == null)
				WeaponSpray.gameObject.SetActive( Fire);
		}
	}

	IEnumerator waitAndDamage(Collider2D target)
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right, Range);
		yield return new WaitForSeconds(timeToHit);
		
		while (hit.collider != null && hit.collider == target) {
			hit = Physics2D.Raycast (transform.position, transform.right, Range);
			if(hit != null)
			{
				if( hit.collider != null && hit.collider == target)
				{
					hit.collider.GetComponent<Enemy>().TakeDamage(Damage);
				}
			}
			yield return new WaitForEndOfFrame();
		}
	}

	void UseSpray()
	{
		var hits = Physics2D.RaycastAll (transform.position, transform.right,Range);
		foreach (var h in hits)
			if (h.collider.tag == "Hittable") {
				Debug.Log(Damage.ToString());			
				h.collider.gameObject.GetComponent<Enemy> ().TakeDamage (Damage);
			}
	}

	void FireWeapon()
	{
		if (Bullet != null) {
			var b = Instantiate (Bullet, transform.position + transform.forward, transform.rotation) as Bullet;
			b.Damage = Damage;// * timeToHit;
			b.Owner = this;
			Destroy (b.gameObject, b.Speed * Range);
		}
	}
}