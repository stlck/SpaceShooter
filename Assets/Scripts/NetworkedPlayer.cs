using UnityEngine;
using System.Collections;

public class NetworkedPlayer : MonoBehaviour {

	public NetworkPlayer PlayerObj;

	public Transform ChosenShip;
	public Transform ChosenWeapon;
	
	public string MyName;
	public bool IsReady = false;
	public Color PlayerColor;

	void Awake()
	{
		if (networkView.viewID.isMine) {
			MyName = PlayerPrefs.GetString ("PName", "P");
			PlayerObj = Network.player;
		} 
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		if (networkView.viewID.isMine) {
			networkView.RPC ("SendMyName", RPCMode.OthersBuffered, MyName);
		}else {
			PlayerObj = networkView.viewID.owner;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerObj == Network.player) {
			networkView.RPC("UpdateMyShip", RPCMode.Others, ChosenShip.position, ChosenShip.eulerAngles);
		}
	}

	[RPC]
	public void UpdateMyShip(Vector3 coordinates, Vector3 eulers)
	{
		ChosenShip.position = coordinates;
		ChosenShip.eulerAngles = eulers;
	}

	[RPC]
	public void SendMyName(string n)
	{
		MyName = n;

		MultiplayerManager.Instance.NetworkReadyToggle.GetComponentInChildren<UnityEngine.UI.Text> ().text = MyName;
	}

	[RPC]
	public void SendReady()
	{
		IsReady = !IsReady;

		if(networkView.viewID.isMine)
			MultiplayerManager.Instance.ReadyToggle.isOn = IsReady;		
		else
			MultiplayerManager.Instance.NetworkReadyToggle.isOn = IsReady;		
	}

	[RPC]
	public void SendShip(int index)
	{
		ChosenShip = MultiplayerManager.Instance.Ships[index];
	}

	[RPC]
	public void SendWeapon(int index)
	{
		ChosenWeapon = MultiplayerManager.Instance.Weapons[index];
	}

	public void StartGame()
	{
		ChosenShip = Instantiate (ChosenShip, ChosenShip.transform.position, ChosenShip.transform.rotation) as Transform;
		ChosenWeapon = Instantiate (ChosenWeapon, ChosenWeapon.transform.position, ChosenWeapon.transform.rotation) as Transform;

		ChosenWeapon.parent = ChosenShip.transform;
		ChosenWeapon.localPosition = Vector3.zero;

		if (PlayerObj != Network.player) {
			ChosenShip.GetComponent<Movement> ().enabled = false;
		} else {
			ChosenShip.GetComponent<Movement> ().enabled = true;
			Movement.Instance = ChosenShip.GetComponent<Movement> ();
		}
	}
	
	void OnLevelWasLoaded(int id)
	{
		if(Application.loadedLevelName == "Game")
			StartGame ();
		if(Application.loadedLevelName == "Menu")
			Destroy (gameObject);
	}
}
