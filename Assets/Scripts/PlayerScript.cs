using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 2f;

	private Rigidbody2D rigidBody;

	private Animator animator;

	private float previousAxis = 0;

	public void Start () {
		this.rigidBody = GetComponent<Rigidbody2D> ();
		this.animator = GetComponent<Animator> ();
	}

	public void Update () {
		var horizontalInput = Input.GetAxisRaw ("Horizontal");
		if (horizontalInput != 0f) {
			// this.animator.SetInteger ("animState", 1);
			this.animator.Play("PlayerWalk");
		} else {
			// this.animator.SetInteger ("animState", 0);
			this.animator.Play("PlayerStill");
		}		
	}

	public void FixedUpdate () {
		var horizontalInput = Input.GetAxisRaw ("Horizontal");
		ManageFacing (horizontalInput);
		ManageSpeed (horizontalInput);
	}

	private void ManageSpeed (float horizontalInput)
	{
		var velocity = this.rigidBody.velocity;
		velocity.x = horizontalInput * speed;
		this.rigidBody.velocity = velocity;
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
