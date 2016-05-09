using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class empezarNivel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void loadlevel()
    {
		SceneManager.LoadScene ("nivelPrueba");
    }
}