using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipGUI : MonoBehaviour {

	public Movement Source;
	
	public Text Name;
	
	public Slider MoveSpeed;
	public Slider RotationSpeed;
	public Slider Health;
	
	// Use this for initialization
	void Start () {
		Name.text = Source.name;

		MoveSpeed.value = Source.Speed;
		RotationSpeed.value = Source.RotationSpeed;
		Health.value = Source.GetComponent<PlayerHealth> ().Health;
	}
}
