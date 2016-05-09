using UnityEngine;
using System.Collections;

public class CambioPlanos : MonoBehaviour {

    public Transform posActual;
    public Transform posFinal;
    public float velocidad = 0.0f;
    public bool finalizador = false; //indica si es el que inicia el evento
    private bool cambio=false;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        cambio = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cambio && !finalizador)
        {
            posActual.position = Vector3.Lerp(posActual.position, posFinal.position, velocidad);
            //rb.MoveRotation(Quaternion.Slerp(posActual.rotation, posFinal.rotation, velocidad));
            posActual.rotation = Quaternion.Slerp(posActual.rotation, posFinal.rotation, velocidad);
            
            if (posActual.rotation == posFinal.rotation)cambio = false;
        }

    }


    void OnTriggerEnter(Collider other)
    {
        cambio = true;
    }
}
