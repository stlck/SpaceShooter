using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour {

    public List<Enemy> Enemies = new List<Enemy>();
    public List<Enemy> Bosses = new List<Enemy>();
	public List<AnimationCurve> Patterns = new List<AnimationCurve>();
	public float SpawnerMoveSpeed = 1;
    public Dictionary<int, List<Enemy>> CurvesEnemies = new Dictionary<int, List<Enemy>>();
	public int Target = 2;
	public float TimeMod = 1f;
	public int WaveCount = 10;

    //private List<Enemy> toRemove = new List<Transform>();
    private List<int> toKill = new List<int>();

	void Awake()
	{
		for(int p = 0; p < Patterns.Count; p++)
            CurvesEnemies.Add(p, new List<Enemy>());
	}

	// Use this for initialization
	void Start () {
		if(Network.peerType == NetworkPeerType.Server || Network.peerType == NetworkPeerType.Disconnected)
			StartSpawn ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (Vector3.right * -SpawnerMoveSpeed * Time.deltaTime);

		foreach (var l in CurvesEnemies.Values) {
            l.RemoveAll(m => m == null);
		}
	}

	void FixedUpdate()
	{
		foreach(var curve in CurvesEnemies)
			foreach (var c in curve.Value)
		{
			if( c != null)
			{
				var pattern = Patterns[curve.Key].Evaluate ( Mathf.Abs(c.transform.position.x) / 14);
				var p =  (Vector2.up * pattern * 6);
				p.y -= c.transform.position.y;
				//c.Translate(p * Time.deltaTime);
				c.rigidbody2D.AddForce(p);

                if (c.transform.position.x <= -15)
					Destroy (c.gameObject);
			}
		}
	}

	public void StartSpawn()
	{
		StartCoroutine (spawner ());
	}

	IEnumerator spawn(int id, int pattern, int count, int EnemyIndex, float delay)
	{
		for (int i = 0; i < count; i++) {
			var at = Instantiate (Enemies[EnemyIndex], Vector3.right * 14 + Vector3.up * 6 * Patterns [pattern].Evaluate (1), Quaternion.identity) as Enemy;
			at.transform.parent = transform;
			CurvesEnemies [pattern].Add (at);
            at.Id = id + i;

			yield return new WaitForSeconds(delay);
		}
	}

	[RPC]
	public void SpawnEnemy(int id, int pattern, int count, int EnemyIndex, float delay)
	{
		StartCoroutine(spawn(id, pattern, count, EnemyIndex, delay));
	}

	IEnumerator spawner()
	{
        var enemyId = 0;
		while (true) 
		{
			var e = GetEnemy();
			var enemy = Enemies[e];
			var enemyCount = Target / enemy.SpawnValue;

			var waves = enemyCount / WaveCount;

			for(int i = 0; i < waves; i++)
			{
				var curveIndex = Random.Range(0, Patterns.Count-1);
                var count = (int)(enemyCount / waves) + Random.Range(-2, 3);
				var delay = enemy.HitPoints * TimeMod;

				if(Network.peerType == NetworkPeerType.Server)
					networkView.RPC("SpawnEnemy", RPCMode.Others, curveIndex, count, e, delay);
                StartCoroutine(spawn(enemyId, curveIndex, count, e, delay));

                enemyId += count;

                yield return new WaitForSeconds(.4f);
                //yield return new WaitForEndOfFrame();
			}

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

    [RPC]
    public void KillEnemy(int id)
    {
        foreach (var l in CurvesEnemies.Values)
        {
            l.ForEach(delegate(Enemy e)
            {
                if (e.Id == id) 
                    e.KillMe();
            });
        }
    }
}
