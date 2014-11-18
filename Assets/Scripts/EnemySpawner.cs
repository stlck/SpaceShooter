using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour {

	public List<Transform> Enemies = new List<Transform>();
	public List<Transform> Bosses = new List<Transform> ();
	public List<AnimationCurve> Patterns = new List<AnimationCurve>();
	public float SpawnerMoveSpeed = 1;
	public Dictionary<int,List<Transform>> CurvesEnemies = new Dictionary<int, List<Transform>>();
	public int Target = 2;
	public float TimeMod = 1f;
	public int WaveCount = 10;

	int enemyId = 0;

	void Awake()
	{
		for(int p = 0; p < Patterns.Count; p++)
			CurvesEnemies.Add (p, new List<Transform> ());
	}

	// Use this for initialization
	void Start () {
		if(Network.peerType == NetworkPeerType.Server || Network.peerType == NetworkPeerType.Disconnected)
			StartSpawn ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (Vector3.right * -SpawnerMoveSpeed * Time.deltaTime);

		var toRemove = new List<Transform> ();
		foreach (var l in CurvesEnemies.Values) {
			foreach (Transform c in l)
				if (c == null)
					toRemove.Add (c);
			foreach (Transform c in toRemove)
				l.Remove (c);

			toRemove = new List<Transform>();
		}
	}

	void FixedUpdate()
	{
		foreach(var curve in CurvesEnemies)
			foreach (var c in curve.Value)
		{
			if( c != null)
			{
				var pattern = Patterns[curve.Key].Evaluate ( Mathf.Abs(c.position.x) / 14);
				var p =  (Vector2.up * pattern * 6);
				p.y -= c.position.y;
				//c.Translate(p * Time.deltaTime);
				c.rigidbody2D.AddForce(p);
				
				if( c.position.x <= -15)
					Destroy (c.gameObject);
			}
		}
	}

	public void StartSpawn()
	{
		StartCoroutine (spawner ());
	}

	IEnumerator spawn(int pattern, int count, int EnemyIndex, float delay)
	{
		for (int i = 0; i < count; i++) {
			var at = Instantiate (Enemies[EnemyIndex], Vector3.right * 14 + Vector3.up * 6 * Patterns [pattern].Evaluate (1), Quaternion.identity) as Transform;
			at.parent = transform;
			CurvesEnemies [pattern].Add (at);
			at.GetComponent<Enemy>().Id = enemyId++;

			yield return new WaitForSeconds(delay);
		}
	}

	[RPC]
	public void SpawnEnemy(int pattern, int count, int EnemyIndex, float delay)
	{
		StartCoroutine(spawn(pattern, count, EnemyIndex, delay));
	}

	IEnumerator spawner()
	{
		while (true) 
		{
			var e = GetEnemy();
			var enemy = Enemies[e].GetComponent<Enemy>();
			var enemyCount = Target / enemy.SpawnValue;

			var waves = enemyCount / WaveCount;

			for(int i = 0; i < waves; i++)
			{
				var curveIndex = Random.Range(0, Patterns.Count-1);
                var count = (int)(enemyCount / waves) + Random.Range(-2, 3);
				var delay = enemy.HitPoints * TimeMod;

				if(Network.peerType == NetworkPeerType.Server || Network.peerType == NetworkPeerType.Disconnected)
					networkView.RPC("SpawnEnemy", RPCMode.Others, curveIndex, count, e, delay);
                StartCoroutine(spawn(curveIndex, count, e, delay));

                yield return new WaitForSeconds(.4f);
                //yield return new WaitForEndOfFrame();
			}

			/*
			do{
				var curveIndex = Random.Range(0, Patterns.Count-1);
				var wave = enemyCount > WaveCount ? WaveCount : enemyCount;
				StartCoroutine(spawn(curveIndex, (int)wave, e, enemy.HitPoints * TimeMod));
				enemyCount -= wave;
				yield return new WaitForEndOfFrame();
			}while(enemyCount > 0);*/

			//SpawnerMoveSpeed = Random.Range(1f,4f);

			Target ++;

			while(CurvesEnemies.Values.Any( m => m.Count > 0))
			{
				yield return new WaitForSeconds(.5f);
			}
		}
	}

	int GetEnemy()
	{
		return Random.Range(0, Enemies.Count);
	}
}
