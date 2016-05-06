using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text timeText;				//Text to show time played
	public Text pauseGUI;				//Text to show when paused
	public Button resetBut;				//Button to show when paused
	public Button exitBut;				//Button to show when paused

	private bool pauseGame = false;		//Variable to know if the game is paused or not
	private bool showGUI = false;		//Variable to determine if we show the pause interface or not

	void Start () {
		timeText.text = "Time: 00:00";
		pauseGUI.text = "";
		resetBut.gameObject.SetActive (false);
		exitBut.gameObject.SetActive (false);
	}
		
	void Update() {
		//=== Time Measurement =====================
		timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("F2");

	}

	//Pauses the game
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
			resetBut.gameObject.SetActive (true);
			exitBut.gameObject.SetActive (true);
		} else {
			pauseGUI.text = "";
			resetBut.gameObject.SetActive (false);
			exitBut.gameObject.SetActive (false);
		}
	}

	//Reset the nivelPrueba level 
	//TO DO: Make it reset tha ACTUAL level
	public void DoReset()
	{
		string aux; 

		//Exit Pause before the reset
		Time.timeScale = 1;
		showGUI = false;
		aux = SceneManager.GetActiveScene ().name;
		//Debug.Log (aux);
		SceneManager.LoadScene(aux);
	}

	//Exist to the main menu
	public void DoExit()
	{
		//Exit Pause before leaving
		Time.timeScale = 1;
		showGUI = false;
		SceneManager.LoadScene("main");
	}
}
