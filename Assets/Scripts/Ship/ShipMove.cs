using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {
	
	Transform thisTransform;
	Rigidbody thisRigidbody;
	
	public float speed = 8.0f;
	public float maxSpeed = 20f;
	public float turnSpeed = 4.0f;
	
	void Start () {	
		thisTransform = transform;
		thisRigidbody = rigidbody;
	}

	void Update (){



	}

	public void Move(Vector3 dir) {

		//transform.Rotate(Vector3.up, dir.x * turnSpeed * Time.deltaTime);

		if (rigidbody.velocity.magnitude > maxSpeed)
			rigidbody.velocity = Vector3.ClampMagnitude (rigidbody.velocity, maxSpeed);
		print (rigidbody.velocity.magnitude);
	}
	
	void FixedUpdate() {
		rigidbody.AddRelativeForce (new Vector3 (0, 0, Input.GetAxis("Vertical") * speed));
		rigidbody.AddRelativeTorque (Vector3.right * turnSpeed * -Input.GetAxis("Horizontal"));
	}
}