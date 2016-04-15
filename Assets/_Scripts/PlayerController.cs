using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

	[HideInInspector] public bool jump = false, attack = false;

	public float speed;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
    public Animator animacion;
	public GameObject arma;

	private BoxCollider areaImpacto;
	private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        animacion = GetComponent<Animator>();
		areaImpacto = arma.gameObject.GetComponent<BoxCollider>(); // Obtener el control de la "hitbox" del arma
    }

    void Update() {
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
            jump = false;
            animacion.SetBool("Jump", jump);
            moveDirection = new Vector3(0, 0, speed);
			moveDirection = transform.TransformDirection(moveDirection);
			if (Input.touchCount == 1) {
				if (Input.GetTouch (0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject ()) {
					moveDirection.y = jumpSpeed;
					jump = true;
					animacion.SetBool ("Jump", jump);
				}
			}
        }

		//Necesitamos manejar el fin de la animación de ataque desde Update()
		//De otro modo, sólo terminaría la animación volviendo a pulsar ataque (¡¡OH, NO!!)
		if (animacion.GetCurrentAnimatorStateInfo(0).IsName("Attack")&&attack){
			attack = false;
			areaImpacto.isTrigger = false;
			Debug.Log("tada2!");
		}
       
        animacion.SetBool("Attack", attack);
        animacion.SetFloat("Speed", speed);
        moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}


	//El ataque puede ser llamado en cualquier momento (cooldown no contemplado)
	public void Ataque () {
		if (!attack)
		{
			attack = true;
			areaImpacto.isTrigger = true;
			Debug.Log("Ataque!");
		}

	}

	public void Ability1 (string text) {
		SceneManager.LoadScene ("main");
	}

}
