﻿using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	private int damage;
    private Health HP;

    public GameObject chispas;

    // Use this for initialization
    void Start () {
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("enemigo tocado "+ col.gameObject.tag);
		if (col.gameObject.tag == "Enemy")
            {
            Debug.Log("impacto");
            HP = col.gameObject.GetComponent<Health>();
            if (HP.sub(damage) > 0)
            {
                Destroy(col.gameObject);
                Instantiate(chispas, transform.position, transform.rotation);
            }

        }
    }


    public void setDamage(int d)
    {
        damage = d;
    }
}
