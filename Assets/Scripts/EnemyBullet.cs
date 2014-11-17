using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	public Transform HitEffect;
	public float damage = .4f;
	public float Speed = 1;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = transform.right * Speed;
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			if(HitEffect != null)
			{
				var go = Instantiate(HitEffect, transform.position, HitEffect.rotation) as Transform;
				Destroy(go.gameObject, 5f);
            }

			PlayerHealth.Instance.Health -= damage;
		}
	}
}
