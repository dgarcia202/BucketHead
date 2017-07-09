using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 2f;

	private Rigidbody2D rigidBody;

	private Animator animator;

	void Start () {
		this.rigidBody = GetComponent<Rigidbody2D> ();
		this.animator = GetComponent<Animator> ();
	}

	void Update () {
		
	}

	void FixedUpdate () {
		var horizontalInput = Input.GetAxisRaw ("Horizontal");
		if (horizontalInput == 0f) {
			this.animator.SetInteger ("AnimationIndex", 1);
		} else {
			this.animator.SetInteger ("AnimationIndex", 0);
		}

		var velocity = this.rigidBody.velocity;
		velocity.x = horizontalInput * speed;
		this.rigidBody.velocity = velocity;
	}
}
