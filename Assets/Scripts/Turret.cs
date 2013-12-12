using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	
	Transform thisTransform;
	BaseInput input;	
	PlayerMove player;
	
	public float turnSpeed = 35.0f;
	
	bool used = false;
	bool playerNear = false;
	
	public GameObject bullet;
	
	public float shootCD = 0.6f;
	
	float shootTimer = 0.0f;
	
	public Transform shootPoint;
	
	CameraFollow cameraFollow;
	
	public Vector3 camOffset = new Vector3(5.0f, 13.0f, 0.0f);
	
	public float maxRotation, minRotation;
	
	public float camSize = 20.0f;
	public Transform camAnchor;
	
	void Start () {
		cameraFollow = Camera.main.GetComponent<CameraFollow>();
		thisTransform = transform;
		player = GameObject.Find("Player").GetComponent<PlayerMove>();
		input = player.GetComponent<PlayerInput>();
		
		if(shootPoint == null)
			shootPoint = thisTransform.FindChild("shootPoint");
			
	}
	
	void Update () {
		if(playerNear) {
			if(Input.GetKeyDown(KeyCode.E)) {
				used = !used;
				player.usingDevice = !player.usingDevice;
				if(used)
				{
					cameraFollow.ChangeCam(camAnchor, camSize);
					Debug.Log ("GunTurret Entered");
				}
				else
				{
					cameraFollow.Reset();
					Debug.Log ("GunTurret Exited");
				}
			}
		}
		
		if(shootTimer > 0.0f) {
			shootTimer -= Time.deltaTime;
		}
		
		if(used) {
			thisTransform.Rotate(Vector3.up, input.dir.x * turnSpeed * Time.deltaTime);
			if(thisTransform.eulerAngles.y < minRotation) {
				thisTransform.rotation = Quaternion.Euler(0.0f, minRotation, 0.0f);
			}
			if(thisTransform.eulerAngles.y > maxRotation) {
				thisTransform.rotation = Quaternion.Euler(0.0f, maxRotation, 0.0f);
			}
			if(input.fire) {
				if(shootTimer <= 0.0f) {
					shootTimer = shootCD;
					Instantiate(bullet, shootPoint.position, thisTransform.rotation);
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
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 150.0f, 40.0f), "Press 'LMB' to fire\n'AD' to turn");
		}
	}
}