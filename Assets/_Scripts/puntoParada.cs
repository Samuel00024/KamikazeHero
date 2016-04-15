using UnityEngine;
using System.Collections;

public class puntoParada : MonoBehaviour {
    private PlayerController pc;
    public GameObject miScript;

    // Use this for initialization
    void Start () {
        pc = miScript.GetComponent<PlayerController>();
    }
    
    // Update is called once per frame
    void Update () {
    
    }
    void OnTriggerEnter(Collider other)
    {
        pc.MovementSpeed = 0;
    }
}
