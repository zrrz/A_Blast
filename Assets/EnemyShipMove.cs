using UnityEngine;
using System.Collections;

public class EnemyShipMove : MonoBehaviour {

	public float maxHealth = 5;
	
	float health;
	
	Transform thisTransform;
	
	public float spinSpeed = 25.0f;
	
	void Start () {
		health = maxHealth;
		thisTransform = transform;
	}
	
	void Update () {
		thisTransform.Rotate(Vector3.up, spinSpeed*Time.deltaTime);
	}
	
	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "PlayerBullet") {
			health--;
			if(health <= 0) {
				Destroy(gameObject);
			}
			Destroy(col.gameObject);
		}
	}
}
