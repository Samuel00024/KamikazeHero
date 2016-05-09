using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class changeLV : MonoBehaviour {

    public int nextLV;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        new WaitForSeconds(5);
        SceneManager.LoadScene(nextLV);

    }

}
