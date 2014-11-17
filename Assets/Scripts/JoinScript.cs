using UnityEngine;
using System.Collections;

public class JoinScript : MonoBehaviour {

	public HostData HD;

	// Use this for initialization
	void Start () {
		transform.name = HD.gameName;
		
		GetComponentInChildren<UnityEngine.UI.Text>().text = HD.gameName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Join()
	{
		MultiplayerLobby.Instance.JoinGame (HD.guid);
	}
}
