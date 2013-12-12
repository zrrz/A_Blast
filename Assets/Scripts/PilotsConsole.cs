using UnityEngine;
using System.Collections;

public class PilotsConsole : MonoBehaviour {

	BaseInput input;	
	PlayerMove player;
	
	bool used = false;
	bool playerNear = false;
	
	CameraFollow cameraFollow;
	
	public float camHeight = 60.0f;
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
				player.usingDevice = !player.usingDevice;
				if(used)
				{
					
					cameraFollow.ChangeCam(camAnchor, camHeight);
				}
				else {
					cameraFollow.Reset();
					ship.Move(Vector3.zero);
				}
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
		if(!used) {
			if(playerNear) {
				GUI.Box(new Rect(0.0f, 0.0f, 150.0f, 50.0f), "Press 'E' to enter");
			}
		}
		
		if(used)  {
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 150.0f, 40.0f), "'WASD' to move");
		}
	}
}
