using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShoot : Enemy {

	public List<Transform> Ordenance;

	public float TimeBetweenShots = 1f;

	// Use this for initialization
	void Start () {
		StartCoroutine (waitAndShoot ());
	}

	IEnumerator waitAndShoot()
	{
		while (true) {
			yield return new WaitForSeconds(TimeBetweenShots);

			foreach(var o in Ordenance)
			{
				var newBullet = Instantiate(o, transform.position, o.rotation) as Transform;
				Destroy (newBullet.gameObject, 10f);
			}
		}
	}
}
