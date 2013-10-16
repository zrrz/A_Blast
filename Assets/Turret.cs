using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	
	Transform thisTransform;
	BaseInput input;	
	PlayerMove player;
	
	public float turnSpeed = 35.0f;
	
	bool used = false;
	bool playerNear = false;
	
	void Start () {
		thisTransform = transform;
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
			thisTransform.Rotate(Vector3.up, input.dir.x * turnSpeed * Time.deltaTime);
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