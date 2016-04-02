using UnityEngine;
using System.Collections;

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
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                jump = true;
                animacion.SetBool("Jump", jump);
            }
        }

        //El ataque pude ser llamado en cualquier momento (cooldown no contemplado)
        if (Input.GetButton("Fire1")&&!attack)
        {
            attack = true;
            areaImpacto.isTrigger = true;
            Debug.Log("tada!");
        }else if (animacion.GetCurrentAnimatorStateInfo(0).IsName("Attack")&&attack){
            attack = false;
            areaImpacto.isTrigger = false;
            Debug.Log("tada2!");
        }
        animacion.SetBool("Attack", attack);
        animacion.SetFloat("Speed", speed);
        moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

}
