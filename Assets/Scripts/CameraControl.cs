using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Transform objectToFollow;

	private float lastKnownPositionX = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var currentPositionX = this.objectToFollow.transform.position.x;

		if (lastKnownPositionX != 0f) {
			var cameraPosition = this.transform.position;
			//cameraPosition.x = (this.objectToFollow.transform.position.x - 5f);
			this.transform.Translate(new Vector3(this.objectToFollow.transform.position.x - lastKnownPositionX, 0f, 0f));
		}

		lastKnownPositionX = currentPositionX;
	}
}
