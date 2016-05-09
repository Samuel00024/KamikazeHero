using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	[HideInInspector] public bool attack = false;

	public GameObject arma;
	public Animator animacion;

	private BoxCollider areaImpacto;

	void Start() {
		areaImpacto = arma.gameObject.GetComponent<BoxCollider>(); // Obtener el control de la "hitbox" del arma
	}

	// Update is called once per frame
	void Update () {
		//El ataque pude ser llamado en cualquier momento (cooldown no contemplado)
		if (!attack)
		{
			attack = true;
			areaImpacto.isTrigger = true;
			Debug.Log("tada!");
		}else if (animacion.GetCurrentAnimatorStateInfo(0).IsName("Attack")&&attack){
			attack = false;
			areaImpacto.isTrigger = false;
			Debug.Log("tada2!");
		}
	}
}
