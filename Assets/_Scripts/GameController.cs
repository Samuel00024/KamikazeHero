using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text timeText;

	void Start () {
		timeText.text = "Time: 00:00";
	}
		
	void Update() {
		//=== Time Measurement =====================
		timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("F2");

	}

}
