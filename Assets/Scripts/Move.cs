using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public Vector2 Direction;
	bool move = false;

	// Use this for initialization
	void Start () {
		StartCoroutine (waitAndMove ());
	}

	void OnMouseEnter()
	{
		move = true;
	}

	void OnMouseDown()
	{
		move = true;
	}

	void OnMouseUp()
	{
		move = false;
	}

	void OnMouseExit()
	{
		move = false;
	}

	IEnumerator waitAndMove()
	{
		while (true) {
			if( move)
				Movement.Instance.Move (Direction);
			yield return new WaitForEndOfFrame();
		}
	}
}
