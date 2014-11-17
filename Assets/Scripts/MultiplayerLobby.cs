using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerLobby : MonoBehaviour {
	private static MultiplayerLobby _instance;
	
	public static MultiplayerLobby Instance{
		get
		{
			return _instance;
		}
	}

	public UnityEngine.UI.InputField NameField;
	public string PNAme;
	public HostData[] Lobbies;
	public static string NetworkGameName = "SpaceStuff";
	public GameObject HostPanel;
	public JoinScript HostTemplate;
	
	void Awake()
	{
		_instance = this;

		MasterServer.ClearHostList();
		MasterServer.RequestHostList(NetworkGameName);

		PNAme = PlayerPrefs.GetString ("PName", "My Name");
		NameField.value = PNAme;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	public void RefreshHostData()
	{
		// read masterserver
		MasterServer.RequestHostList(NetworkGameName);
	}
	
	// Update is called once per frame
	public void JoinGame(string guid)
	{
		// connect, change scene
		Network.Connect (guid);
		Debug.Log ("Joined ");
		LoadSetupScene ();
	}

	public void HostGame()
	{
		Network.InitializeServer(2, 25000, !Network.HavePublicAddress());

	}

	void OnServerInitialized() {
		MasterServer.RegisterHost(NetworkGameName, "My Game Instance", "");

		LoadSetupScene ();
	}

	void LoadSetupScene()
	{
		Application.LoadLevel ("MpSetup");
		
	}

	public void SetPlayerName()
	{
		PNAme = NameField.value;
		PlayerPrefs.SetString ("PName", PNAme);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.RegistrationSucceeded)
			Debug.Log("Server registered");
		if (msEvent == MasterServerEvent.HostListReceived) {
			Lobbies = MasterServer.PollHostList ();
			SetupLobbies();
		}
	}

	void SetupLobbies()
	{
		var children = new List<GameObject>();
		foreach (Transform child in HostPanel.transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));

		for (int i = 0; i < Lobbies.Length; i++) {
			Debug.Log(Lobbies[i].gameName + " " + Lobbies[i].guid);
			var c = Instantiate(HostTemplate) as JoinScript;
			c.transform.parent = HostPanel.transform;
			c.HD = Lobbies[i];
		}
	}
}
