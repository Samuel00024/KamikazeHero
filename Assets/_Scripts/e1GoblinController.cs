using UnityEngine;
using System.Collections;

public class e1GoblinController : MonoBehaviour {

	public float speed;
	public float gravity = 20.0f;

	public Vector3 moveDirection = Vector3.zero;
	
	// Update is called once per frame
	void Update () {
		moveDirection = new Vector3 (-speed, 0, 0);
		moveDirection.y -= gravity * Time.deltaTime;
		transform.Translate (moveDirection * Time.deltaTime);
	}
}
