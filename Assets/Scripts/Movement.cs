using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public static Movement Instance;
	public Bounds bounds;
	public float Speed = 2f;
	public float RotationSpeed = 5f;
	public Weapon ShipWeapon;

	void Awake()
	{
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W))
			Move (Vector3.up);
		else if (Input.GetKey (KeyCode.S))
			Move (Vector3.down);
		
		/*if (Input.GetMouseButton (0)) {
			var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(p.x > 0)
			{
				Movement.Instance.Rotate(p);
				//Weapon.Instance.Fire = true;
			}
		}*/

	}

	public void Move(Vector3 dir)
	{
		var pos = transform.position + dir * Time.deltaTime * Speed;
		if(bounds.Contains( pos))
			transform.position = pos;
	}

	public void Rotate(Vector3 target)
	{
		var v3Dir = target - transform.position;
		var angle = Mathf.Atan2(v3Dir.y, v3Dir.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, angle, Time.deltaTime * RotationSpeed));
	}
}
