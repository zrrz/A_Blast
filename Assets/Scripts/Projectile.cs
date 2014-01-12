using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {


	public float speed; 


	public int dmg; 
	public float lifeTime;


	void Start () {
	
	}
	
	void FixedUpdate () {
	
		transform.position += transform.forward * speed * Time.deltaTime;

	}
	
	public void SetProjectile (Vector3 pos, Quaternion rot){

		enabled = true;
		transform.position = pos;
		transform.rotation = rot;

	}
	
	void Disable (){

		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		enabled = false;

	}

}
