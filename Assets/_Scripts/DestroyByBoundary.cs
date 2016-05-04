using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if (other.tag == "Enemy") 
			Destroy (other.gameObject);	
	}
}
