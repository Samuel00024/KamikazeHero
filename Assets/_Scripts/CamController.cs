using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CamController : MonoBehaviour {

    public Transform posActual;
    public float velocidad = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.Lerp(transform.position, posActual.position, velocidad);
        transform.rotation = Quaternion.Slerp(transform.rotation, posActual.rotation, velocidad);
	}

    public void setPoss (Transform poss)
    {
        posActual = poss;
    }
    public void startLevel()
    {
		SceneManager.LoadScene ("nivelPrueba");
    }
}
