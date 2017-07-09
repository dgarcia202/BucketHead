using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 2f;

	public float jumpForce = 10f;

	private Rigidbody2D rigidBody;

	private Animator animator;

	private float previousAxis = 0;

	public void Start () {
		this.rigidBody = GetComponent<Rigidbody2D> ();
		this.animator = GetComponent<Animator> ();
	}

	public void Update () {
	}

	public void FixedUpdate () {
		var horizontalInput = Input.GetAxisRaw ("Horizontal");

		ManageAnimations (horizontalInput);
		ManageFacing (horizontalInput);
		ManageSpeed (horizontalInput);

		var velocity = this.rigidBody.velocity;
		if (Input.GetButtonDown ("Jump") && velocity.y == 0) {
			velocity.y = jumpForce;
			this.rigidBody.velocity = velocity;
		}
	}

	private void ManageSpeed (float horizontalInput)
	{
		var velocity = this.rigidBody.velocity;
		velocity.x = horizontalInput * speed;
		this.rigidBody.velocity = velocity;
	}

	void ManageAnimations (float horizontalInput)
	{
		var currentAnimation = this.animator.GetCurrentAnimatorStateInfo (0);
		/*
		if (horizontalInput != 0f && !currentAnimation.IsName ("PlayerAttack")) {
			this.animator.Play ("PlayerWalk");
		}
		else {
			this.animator.Play ("PlayerStill");
		}*/

		if (Input.GetButtonDown ("Fire1")) {
			this.animator.Play ("PlayerAttack");
		}
	}

	private void ManageFacing (float horizontalInput)
	{
		var localScale = this.transform.localScale;
		if (previousAxis <= 0 && horizontalInput > 0) {
			localScale.x = 1;
		}
		else
			if (previousAxis >= 0 && horizontalInput < 0) {
				localScale.x = -1;
			}
		this.previousAxis = horizontalInput;
		this.transform.localScale = localScale;
	}
}
