using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int Id = -1;
	public int Points = 100;
	//public Transform HealthBar;
	public Transform DeathEffect;
	public float HitPoints = 1f;
	public float SpawnValue = 1f;
	float hpBase;
	SpriteRenderer spr;
	Color baseColor;

	void Awake()
	{
		hpBase = HitPoints;
		spr = GetComponent<SpriteRenderer> ();
		baseColor = spr.color;

		/*HealthBar = Instantiate (HealthBar, transform.position + Vector3.up, HealthBar.rotation) as Transform;
		HealthBar.parent = transform;
		HealthBar.gameObject.SetActive (false);*/
	}
	
	// Update is called once per frame
	void Update () {

		spr.color = Color.Lerp(baseColor, baseColor /2, 1 -( HitPoints / hpBase));

		if (HitPoints <= 0) {
			if(DeathEffect != null)
			{
				var e = Instantiate(DeathEffect, transform.position, DeathEffect.rotation) as Transform;
				Destroy (e.gameObject, 10);
			}
						
			if(Scoring.Instance != null)
				Scoring.Instance.AddScore((int)(Points * hpBase), transform.position);

			Death();
		}
	}

	public virtual void Death()
	{
		Destroy (gameObject);
	}

	public void TakeDamage(float damage)
	{
		/*HealthBar.gameObject.SetActive (true);*/
		HitPoints -= damage;
		/*HealthBar.localScale = new Vector3 (HitPoints / hpBase, 1, 1);*/
	}

	public IEnumerator sendDamage( float wait, float dam)
	{
		yield return new WaitForSeconds(wait);
		TakeDamage (dam);
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.tag == "Player2") {
			/*HealthBar.gameObject.SetActive (true);*/
			HitPoints -= Time.deltaTime;
			/*HealthBar.localScale = new Vector3 (HitPoints / hpBase, 1, 1);*/
		}
	}
}
