using UnityEngine;
using System.Collections;

public class e1GoblinController : MonoBehaviour {

	private float speed = 5;
	private int damage = 1;

	[Header("Parameters")]
	public float gravity = 10.0F;

	[Header("References")]
	public Transform Player;

	private GameController gameController;
	private Vector3 moveDirection = Vector3.zero;


	void Start () {

	}


	void Update () {
		//moveDirection = new Vector3(Player.velocity.x, 0, Player.velocity.z); //Player velocity so we know the direction
		moveDirection = new Vector3((-1)*Player.forward.x, 0, (-1)*Player.forward.z);
		moveDirection.y -= gravity * Time.deltaTime;
		transform.Translate (moveDirection * speed  * Time.deltaTime);
	}
		
}
