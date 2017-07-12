using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	/// <summary>
	/// The speed.
	/// </summary>
	public float speed = 2f;

	/// <summary>
	/// The jump force.
	/// </summary>
	public float jumpForce = 10f;

	public bool isGrounded = false;

	public LayerMask playerMask;

	private Rigidbody2D rigidBody;

	private Animator animator;

	private float previousAxis = 0;

	private Transform groundDetect;

	/// <summary>
	/// Start this instance.
	/// </summary>
	public void Start () {
		this.rigidBody = GetComponent<Rigidbody2D> ();
		this.animator = GetComponent<Animator> ();
		this.groundDetect = GameObject.Find (this.name + "/ground_detect").transform;
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update () {
	}

	/// <summary>
	/// Fixeds the update.
	/// </summary>
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

		this.isGrounded = Physics2D.Linecast (this.transform.position, this.groundDetect.position, this.playerMask);
		this.animator.SetBool ("isJumping", !this.isGrounded);
	}

	private void ManageSpeed (float horizontalInput)
	{
		var velocity = this.rigidBody.velocity;
		velocity.x = horizontalInput * speed;
		this.rigidBody.velocity = velocity;
	}

	void ManageAnimations (float horizontalInput)
	{
		
		if (horizontalInput != 0f) {
			this.animator.SetBool ("isWalking", true);
		}
		else {
			this.animator.SetBool ("isWalking", false);
		}

		if (Input.GetButtonDown ("Fire1")) {
			this.animator.SetBool ("isAttacking", true);
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
