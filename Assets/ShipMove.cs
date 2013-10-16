using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {
	
	Transform shipTransform;
	BaseInput input;	
	PlayerMove player;
	
	public float speed = 5.0f;
	public float turnSpeed = 5.0f;
	
	bool used = false;
	bool playerNear = false;
	
	void Start () {
		shipTransform = transform.parent;
		player = GameObject.Find("Player").GetComponent<PlayerMove>();
		input = player.GetComponent<PlayerInput>();
	}
	
	void Update () {
		if(playerNear) {
			if(Input.GetKeyDown(KeyCode.E)) {
				used = !used;
				player.used = !player.used;
			}
		}
		if(used) {
			shipTransform.position += shipTransform.TransformDirection(0.0f, 0.0f, input.dir.z * speed * Time.deltaTime);
			shipTransform.Rotate(Vector3.up, input.dir.x * turnSpeed * Time.deltaTime);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			playerNear = true;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Player") {
			playerNear = false;
		}
	}
	
	void OnGUI() {
		if(playerNear) {
			GUI.Box(new Rect(0.0f, 0.0f, 150.0f, 50.0f), "Press 'E' to enter");
		}
	}
}