using UnityEngine;
using System.Collections;

public class GravityConsole : MonoBehaviour {
	
	BaseInput input;	
	PlayerMove player;
	
	bool used = false;
	bool playerNear = false;
	
	public float camSize = 60.0f;
	public Transform camAnchor;
	
	CameraFollow cameraFollow;
	
	public GameObject asteroidGrabber;
	
	// Use this for initialization
	void Start () {
		
		cameraFollow = Camera.main.GetComponent<CameraFollow>();
		player = GameObject.Find("Player").GetComponent<PlayerMove>();
		input = player.GetComponent<PlayerInput>();
		asteroidGrabber.GetComponent<AsteroidGrabber>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if(playerNear) {
			if(Input.GetKeyDown(KeyCode.E)) {
				used = !used;
				player.UsingDevice = !player.UsingDevice;
				if(used)
				{
					cameraFollow.ChangeCam(camAnchor, camSize);
					Debug.Log ("Gravity Console Entered");
				}
				else
				{
					cameraFollow.Reset();
					Debug.Log ("Gravity Console Exited");
				}
			}
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
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 150.0f, 40.0f), "Use ASWD to move the gravity field");
		}
	}
}
