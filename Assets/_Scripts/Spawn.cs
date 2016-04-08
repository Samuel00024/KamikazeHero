using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    public GameObject prefab, posicion;
	void Start () {
	}

    void OnTriggerEnter(Collider other)
    {
        Instantiate(prefab, posicion.transform.position, Quaternion.Euler(posicion.transform.eulerAngles));
    }
}
