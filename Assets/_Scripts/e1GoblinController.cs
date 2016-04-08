using UnityEngine;
using System.Collections;

public class e1GoblinController : MonoBehaviour {

	public float speed;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;

	void Update () {
		moveDirection = new Vector3(-speed, 0, 0);
		moveDirection.y -= gravity * Time.deltaTime;
		transform.Translate (moveDirection * Time.deltaTime);
	}
}
