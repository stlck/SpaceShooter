using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoring : MonoBehaviour {

	public static Scoring Instance;
	public Text CurrentScoreLabel;
	public Text AddedScoreLabel;
	public int CurrentScore = 0;
	public float HideAfter = 1;

	// Use this for initialization
	void Start () {
		Instance = this;
		AddedScoreLabel.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		CurrentScoreLabel.text = CurrentScore.ToString ();
	}

	public void AddScore(int points, Vector3 position)
	{
		StopAllCoroutines ();
		CurrentScore += points;

		AddedScoreLabel.text = points.ToString ();
		AddedScoreLabel.rectTransform.position = Camera.main.WorldToScreenPoint(position);

		StartCoroutine (waitAndHide ());
	}

	IEnumerator waitAndHide()
	{
		AddedScoreLabel.gameObject.SetActive (true);
		float timer = 0f;
		while (timer <= HideAfter) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;
			AddedScoreLabel.rectTransform.anchoredPosition += Vector2.up;
			AddedScoreLabel.fontSize = (int)( 16 + timer * 16);
		}
		AddedScoreLabel.text = "";
		AddedScoreLabel.gameObject.SetActive (false);
	}
}
