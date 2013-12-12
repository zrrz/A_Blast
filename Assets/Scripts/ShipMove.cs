using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {
	
	Transform thisTransform;
	Rigidbody thisRigidbody;
	
	public float speed = 8.0f;
	public float turnSpeed = 4.0f;
	
	void Start () {	
		thisTransform = transform;
		thisRigidbody = rigidbody;
	}
	
	public void Move(Vector3 dir) {
		thisTransform.position += transform.TransformDirection(new Vector3(0.0f, 0.0f, dir.z)) * speed * Time.deltaTime;
		thisTransform.Rotate(Vector3.up, dir.x * turnSpeed * Time.deltaTime);
	}
	
	void FixedUpdate() {
		thisRigidbody.angularVelocity = Vector3.zero;
		thisRigidbody.velocity = Vector3.zero;
	}
}