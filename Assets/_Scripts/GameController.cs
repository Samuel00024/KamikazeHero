using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text timeText;				//Text to show time played
	public Text pauseGUI;				//Text to show when paused

	private bool pauseGame = false;		//Variable to know if the game is paused or not
	private bool showGUI = false;		//Variable to determine if we show the pause interface or not

	void Start () {
		timeText.text = "Time: 00:00";
		pauseGUI.text = "";
	}
		
	void Update() {
		//=== Time Measurement =====================
		timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("F2");

	}

	public void DoPause()
	{
		//=== Pause ===============================
		pauseGame = !pauseGame;		//State change from paused to not paused and reverse

		if (pauseGame == true) {
			Time.timeScale = 0;

			showGUI = true;
		} else {
			Time.timeScale = 1;
			showGUI = false;
		}

		if (showGUI) {
			pauseGUI.text = "Pause";
		} else {
			pauseGUI.text = "";
		}
	}

}
