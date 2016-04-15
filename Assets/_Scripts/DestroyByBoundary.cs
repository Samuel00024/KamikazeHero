using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		//Destruye a los enemigos que salgan de la pantalla
		if (other.tag == "Enemy")
			Destroy (other.gameObject);
	}
}
