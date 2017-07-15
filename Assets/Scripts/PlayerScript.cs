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
	public float jumpForce = 15f;

	/// <summary>
	/// The jump boost.
	/// </summary>
	public float jumpBoost = 2f;

	/// <summary>
	/// The player mask.
	/// </summary>
	public LayerMask playerMask;

	private Rigidbody2D rigidBody;

	private Animator animator;

	private float previousAxis = 0;

	private Transform groundDetect;

	private bool isGrounded = false;

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
		this.isGrounded = Physics2D.Linecast (this.transform.position, this.groundDetect.position, this.playerMask);

		Animations (horizontalInput);
		Flip (horizontalInput);
		Walking (horizontalInput);
		Jump ();
	}

	private void Walking (float horizontalInput)
	{
		var velocity = this.rigidBody.velocity;
		velocity.x = horizontalInput * speed * (!this.isGrounded ? this.jumpBoost : 1);
		this.rigidBody.velocity = velocity;
	}

	void Animations (float horizontalInput)
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

		this.animator.SetBool ("isJumping", !this.isGrounded);
	}

	void Jump()	{
		if (Input.GetButtonDown ("Jump") && this.isGrounded) {
			this.rigidBody.velocity = new Vector2 (this.rigidBody.velocity.x * this.jumpBoost, jumpForce);
		}
	}

	private void Flip (float horizontalInput)
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
