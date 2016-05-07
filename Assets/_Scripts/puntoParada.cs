using UnityEngine;

using System.Collections;



public class puntoParada : MonoBehaviour {

    private KamiController pc;

    public GameObject miScript;



    // Use this for initialization

    void Start () {

        pc = miScript.GetComponent<KamiController>();

    }

    

    // Update is called once per frame

    void Update () {

    

    }

    void OnTriggerEnter(Collider other)

    {

        pc.MovementSpeed = 0;

    }

}

