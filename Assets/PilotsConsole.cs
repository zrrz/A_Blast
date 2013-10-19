using UnityEngine;
using System.Collections;

public class PilotsConsole : MonoBehaviour {

	BaseInput input;	
	PlayerMove player;
	
	bool used = false;
	bool playerNear = false;
	
	CameraFollow cameraFollow;
	
	public float camSize = 17.0f;
	public Transform camAnchor;
	
	public ShipMove ship;
	
	void Start () {
		cameraFollow = Camera.main.GetComponent<CameraFollow>();
		player = GameObject.Find("Player").GetComponent<PlayerMove>();
		input = player.GetComponent<PlayerInput>();
		
		if(ship == null)
			ship = transform.parent.GetComponent<ShipMove>();
		
	}
	
	void Update () {
		if(playerNear) {
			if(Input.GetKeyDown(KeyCode.E)) {
				used = !used;
				player.used = !player.used;
				if(used)
					cameraFollow.ChangeCam(camAnchor, camSize);
				else
					cameraFollow.Reset();
			}
		}
		if(used) {
			ship.Move(input.dir);
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
		
		if(used)  {
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 150.0f, 40.0f), "Press 'LMB' to fire\n'WASD' to move");
		}
	}
}
