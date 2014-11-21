using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public static Movement Instance;
	public Bounds bounds;
	public float Speed = 2f;
	public float RotationSpeed = 5f;
	public Weapon ShipWeapon;

	public LineRenderer Line;
	public Gradient LineGrad;

	float angleOffset;
	Color startColor;
	void Awake()
	{
		Instance = this;

		if (Line == null)
			Line = GetComponentInChildren<LineRenderer> ();

		
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
		//Line.SetPosition (0, transform.position);
		//Line.SetPosition (1, transform.right * 10);
		var a = 1 - Mathf.Abs( Mathf.DeltaAngle((angleOffset),(transform.eulerAngles.z)))/RotationSpeed;
		Debug.Log (a);
		Line.SetColors (LineGrad.Evaluate ( Mathf.Abs( a)), LineGrad.Evaluate (1));
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
		angleOffset = Mathf.Atan2(v3Dir.y, v3Dir.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, angleOffset, Time.deltaTime * RotationSpeed));
	}
}
