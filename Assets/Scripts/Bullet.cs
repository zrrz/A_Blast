using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	Transform thisTransform;
	Rigidbody thisRigidbody;

	public float damage = 20.0f;
	
	public float speed = 60.0f;
	
	void Start () {
		thisTransform = transform;
		thisRigidbody = rigidbody;
		thisRigidbody.AddForce(thisTransform.forward * speed, ForceMode.VelocityChange);
		Destroy(gameObject, 5.0f);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		collision.gameObject.SendMessage ("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		gameObject.SetActive(false);
		//if(collision.gameObject.tag == "PlayerShip")
		//{
		//	gameObject.SetActive(false);
		//	collision.gameObject.GetComponent<Health>().m_health -= 20;
		//}
	}
}
