using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponGUI : MonoBehaviour {

	public Weapon Source;

	public Text Name;

	public Slider Damage;
	public Slider ROF;
	public Toggle AOE;

	// Use this for initialization
	void Start () {
		Name.text = Source.name;
		Damage.value = Source.Damage;
		ROF.value = Source.timeToHit;
		AOE.isOn = Source.Bullet.Aoe;
	}
}
