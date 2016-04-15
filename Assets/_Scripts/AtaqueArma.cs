using UnityEngine;
using System.Collections;

public class AtaqueArma : MonoBehaviour {

	public GameObject chispas;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Player")
        {
            Destroy(col.gameObject);
			Instantiate(chispas, transform.position, transform.rotation);
            Debug.Log("bum!");
        }
        else
        {
            Debug.Log("no tada");
        }


    }

}
