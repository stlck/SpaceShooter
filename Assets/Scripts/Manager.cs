using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	public static Manager Instance;
	public List<GameObject> SpawnOnStart = new List<GameObject>();

	public int Score = 0;


	public Transform ChosenWeapon;
	public Transform ChosenShip;

	void Awake()
	{
			Instance = this;
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}

	public void ShipSelect(Transform ship)
	{
		ChosenShip = ship;
	}

	public void WeaponSelect(Transform weapon)
	{
		ChosenWeapon = weapon;
	}

	public void LoadScene(string name)
	{
		Application.LoadLevel (name);
	}

	public void StartGame()
	{
		ChosenShip = Instantiate (ChosenShip, ChosenShip.transform.position, ChosenShip.transform.rotation) as Transform;
		ChosenWeapon = Instantiate (ChosenWeapon, ChosenWeapon.transform.position, ChosenWeapon.transform.rotation) as Transform;

		ChosenShip.GetComponent<Movement> ().enabled = true;
	}

	void OnLevelWasLoaded(int id)
	{
		if(Application.loadedLevelName == "Game")
			StartGame ();
		if(Application.loadedLevelName == "Menu")
			Destroy (gameObject);
	}

	public static void EndGame()
	{
		//Score = Scoring.Instance.CurrentScore;
		if (Instance != null)
						Instance.StartCoroutine (Instance.waitAndLoad ("Score"));
				else
						Application.LoadLevel ("Score");
	}
	
	IEnumerator waitAndLoad(string name)
	{
		yield return new WaitForSeconds(.5f);
		Application.LoadLevel(name);
	}
}
