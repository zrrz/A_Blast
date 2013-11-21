using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {
	
	Transform thisTransform;
	Rigidbody thisRigidbody;
	
	public float speed = 5.0f;
	public float turnSpeed = 5.0f;
	
	Vector3 targetVelocity;
	//Vector3 targetAngularVelocity;
	
	public float maxVelocityChange = 10.0f;
	//public float maxAngularVelocityChange = 10.0f;
	
	void Start () {	
		thisTransform = transform;
		thisRigidbody = rigidbody;
	}
	
	public void Move(Vector3 dir) {
		targetVelocity = thisTransform.TransformDirection(0.0f, 0.0f, dir.z * speed);
		//targetAngularVelocity = new Vector3(0.0f, dir.x * turnSpeed, 0.0f);
		thisTransform.Rotate(Vector3.up, dir.x * turnSpeed * Time.deltaTime);
	}
	
	void FixedUpdate() {
		Vector3 velocityChange = targetVelocity - thisRigidbody.velocity;
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		thisRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		
		thisRigidbody.angularVelocity = Vector3.zero;
		
		//Vector3 angularVelocityChange = targetAngularVelocity - thisRigidbody.angularVelocity;
		//angularVelocityChange.y = Mathf.Clamp(angularVelocityChange.y, -maxAngularVelocityChange, maxAngularVelocityChange);
		//thisRigidbody.AddTorque(angularVelocityChange, ForceMode.VelocityChange);
	}
}