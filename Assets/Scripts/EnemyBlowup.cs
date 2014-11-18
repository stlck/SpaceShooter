using UnityEngine;
using System.Collections;

public class EnemyBlowup : Enemy {

	public float DeathHitRange = 3;
	public float DeathDamage =.4f;

	public override void Death ()
	{
		var coll = Physics2D.OverlapCircleAll(transform.position, DeathHitRange);
		
		foreach(var c in coll)
			if(c.gameObject.tag == "Player")
		{
			var e = c.gameObject.GetComponent<PlayerHealth>();
			var dist = Vector3.Distance(transform.position, c.transform.position) * .1f;
			e.Health -= DeathDamage;
		}

		base.Death ();
	}
}
