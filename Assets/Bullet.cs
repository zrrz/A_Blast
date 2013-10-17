using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	Transform thisTransform;
	Rigidbody thisRigidbody;
	
	public float speed = 60.0f;
	
	void Start () {
		thisTransform = transform;
		thisRigidbody = rigidbody;
		thisRigidbody.AddForce(thisTransform.forward * speed, ForceMode.VelocityChange);
		Destroy(gameObject, 5.0f);
	}
	
	void Update () {
		
	}
}
