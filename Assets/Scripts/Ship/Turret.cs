using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret : MonoBehaviour {

	public int cameraHeight;
	public float viewDistance;


	public float maxLeftAngle = 30;
	public float maxRightAngle = 30;
	public float turnSpeed = 35.0f;

	
	bool used = false;
	bool playerNear = false;
	GameObject nearPlayer;

	public GameObject bullet;
	public float shootCD = 0.6f;
	float shootTimer = 0.0f;
	
	public Transform shootPoint;

	public Transform[] spawnPoins;
	public int spawnPointIndex;

	CameraFollow cameraFollow;
	
	public Vector3 camOffset = new Vector3(5.0f, 13.0f, 0.0f);

	public float maxRotation, minRotation;
	
	public float camSize = 80.0f;
	public Transform camAnchor;


	public List<GameObject> projectiles = new List<GameObject>();
	public int projectileIndex;

	
	void Start () {
		cameraFollow = Camera.main.GetComponent<CameraFollow>();
		GenerateNewProjectiles(bullet, (int)(1+(1/shootCD)));
	}
	
	void Update () {
		if(playerNear) {
			if(Input.GetKeyDown(KeyCode.E)) {
				used = !used;
				if(used)
				{
					cameraFollow.ChangeCam(camAnchor, cameraHeight, viewDistance);
					nearPlayer.GetComponent<PlayerMove>().usingDevice = true;
					Debug.Log ("GunTurret Entered");
				}
				else
				{
					cameraFollow.Reset();
					nearPlayer.GetComponent<PlayerMove>().usingDevice = false;
					Debug.Log ("GunTurret Exited");
				}
			}
		}
		
		if(shootTimer > 0.0f) {
			shootTimer -= Time.deltaTime;
		}
		
		if(used) {

			networkView.RPC("Rotate", RPCMode.All, transform.rotation);

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit, 500);

			shootPoint.position = new Vector3 (hit.point.x, transform.position.y, hit.point.z);

			Quaternion lookRotation = Quaternion.LookRotation (shootPoint.position - transform.position);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
			transform.localEulerAngles = new Vector3 (0, Mathf.Clamp(transform.localEulerAngles.y, maxLeftAngle, maxRightAngle), 0);

			if(Input.GetButton("Fire1")) {
				if(shootTimer <= 0.0f) {
					shootTimer = shootCD;
					networkView.RPC("ShootProjectile", RPCMode.All);	
				}
			}
		}		
	}

	[RPC]
	void ShootProjectile (){

		print ("Shooting");
		projectileIndex++;
		if (projectileIndex == projectiles.Count)
			projectileIndex = 0;

		spawnPointIndex++;
		if (spawnPointIndex == spawnPoins.Length)
			spawnPointIndex = 0;

		projectiles[projectileIndex].GetComponent<Projectile>().SetProjectile(spawnPoins[spawnPointIndex].position, spawnPoins[spawnPointIndex].rotation);	
		
	}

	[RPC]
	void Rotate (Quaternion rot){

		transform.rotation = Quaternion.Lerp (transform.rotation, rot, 0.1f);

	}

	void GenerateNewProjectiles (GameObject projectile, int count){
		
		for (int p=0; p<count; p++){
			
			GameObject newProjectile = Instantiate (projectile, Vector3.zero, Quaternion.identity) as GameObject;
			projectiles.Add (newProjectile);
			
		}

		projectileIndex = 0;

	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			playerNear = true;
			nearPlayer = other.gameObject;
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