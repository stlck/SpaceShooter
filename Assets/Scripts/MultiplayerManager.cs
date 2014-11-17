using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerManager : MonoBehaviour {

	private static MultiplayerManager _instance;

	public static MultiplayerManager Instance{
		get
		{
			return _instance;
		}
	}

	public GameObject ShipSelect;

	public List<Transform> Ships = new List<Transform> ();
	public List<Transform> Weapons = new List<Transform>();

	public NetworkedPlayer Player;
	public NetworkedPlayer PlayerTemplate;
	public UnityEngine.UI.Toggle ReadyToggle;
	public UnityEngine.UI.Toggle NetworkReadyToggle;


	void Awake()
	{
		_instance = this;
		ShipSelect.SetActive (false);
		//DontDestroyOnLoad (gameObject);
	}

	void Update()
	{
		if (Network.peerType == NetworkPeerType.Server && ReadyToggle.isOn && NetworkReadyToggle.isOn) {
			networkView.RPC("StartGame", RPCMode.All);
		}
	}

	public void SetShip(int index)
	{
		Player.networkView.RPC ("SendShip", RPCMode.All, index);
	}
	
	public void SetWeapon(int index)
	{
		Player.networkView.RPC ("SendWeapon", RPCMode.All, index);
	}

	public void ToggleReady()
	{
		Player.networkView.RPC("SendReady", RPCMode.Others);
	}

	public void SetColor(float val)
	{
		Color c = new Color (val,val + .2f,1f);
		ReadyToggle.image.color = c;
		Player.PlayerColor = c;
	}

	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + " connected from " + player.ipAddress + ":" + player.port);

		MasterServer.UnregisterHost ();
		Player = Network.Instantiate (PlayerTemplate, Vector3.zero, Quaternion.identity, 0) as NetworkedPlayer;
		
		ShipSelect.SetActive (true);
	}

	void OnConnectedToServer() {
		Debug.Log("Connected to server");
		Player = Network.Instantiate (PlayerTemplate, Vector3.zero, Quaternion.identity, 0) as NetworkedPlayer;
		ShipSelect.SetActive (true);
	}

	[RPC]
	public void StartGame()
	{
		Application.LoadLevel ("Game");
	}

}
