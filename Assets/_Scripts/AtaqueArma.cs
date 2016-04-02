using UnityEngine;
using System.Collections;

public class AtaqueArma : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Player")
        {
            Destroy(col.gameObject);
            Debug.Log("bum!");
        }
        else
        {
            Debug.Log("no tada");
        }


    }

}
