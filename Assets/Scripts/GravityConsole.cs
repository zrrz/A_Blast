using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityConsole : MonoBehaviour {
	
	BaseInput input;	
	PlayerMove player;
	
	public bool used = false;
	bool playerNear = false;
	
	public float camSize = 60.0f;
	public Transform camAnchor;
	
	public Vector3 m_position;
	public Vector3 newforce;
	public int maxForce = 2500;
	
	CameraFollow cameraFollow;
	
	public List<GameObject> asteroidsGrabbedList;
	public GameObject asteroidGrabber;
	
	// Use this for initialization
	void Start () {
		
		cameraFollow = Camera.main.GetComponent<CameraFollow>();
		player = GameObject.Find("Player").GetComponent<PlayerMove>();
		input = player.GetComponent<PlayerInput>();
		asteroidGrabber.GetComponent<AsteroidGrabber>();
		m_position = asteroidGrabber.transform.position;
	
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
		
		if(used)
		{
			if(asteroidsGrabbedList.Count == 0)
			{
		
				if(Input.GetKey(KeyCode.A))
				{
					m_position += new Vector3(-4,0,0);
				}
				if(Input.GetKey(KeyCode.D))
				{
					m_position += new Vector3(4,0,0);
				}
				if(Input.GetKey(KeyCode.W))
				{
					m_position += new Vector3(0,0,4);
				}
				if(Input.GetKey(KeyCode.S))
				{
					m_position += new Vector3(0,0,-4);
				}
				if(Input.GetKeyDown(KeyCode.Space))
				{
					Collider[] collisions = Physics.OverlapSphere(asteroidGrabber.transform.position, ((SphereCollider)asteroidGrabber.collider).radius * asteroidGrabber.transform.localScale.x);
					foreach(Collider C in collisions)
					{
						if(C.gameObject.tag == "Asteroid")
						{
							asteroidsGrabbedList.Add(C.gameObject);
							C.gameObject.rigidbody.velocity = new Vector3(0,0,0);
							Debug.Log (C.gameObject.rigidbody.velocity);
						}
					}
				}
			}
			//direction
			if(Input.GetKey(KeyCode.Space) && asteroidsGrabbedList.Count > 0)
			{
				Debug.Log ("adding directional forces: " + newforce);
				
				if(Input.GetKeyDown(KeyCode.A))
				{
					newforce += new Vector3(-200,0,0);
					Debug.Log ("adding directional forces: " + newforce);
				}
				if(Input.GetKeyDown(KeyCode.D))
				{
					newforce += new Vector3(200,0,0);
					Debug.Log ("adding directional forces: " + newforce);
				}
				if(Input.GetKeyDown(KeyCode.W))
				{
					newforce += new Vector3(0,0,200);
					Debug.Log ("adding directional forces: " + newforce);
				}
				if(Input.GetKeyDown(KeyCode.S))
				{
					newforce += new Vector3(0,0,-200);
					Debug.Log ("adding directional forces: " + newforce);
				}
			}
			
			if(Input.GetKeyUp(KeyCode.Space))
			{
				if(newforce.x > maxForce)
					newforce.x = maxForce;
				if(newforce.z > maxForce)
					newforce.z = maxForce;
				foreach(GameObject asteroid in asteroidsGrabbedList)
					asteroid.rigidbody.AddForce(newforce);
				
				asteroidsGrabbedList.Clear();
				newforce.Set(0,0,0);
			}
			
			if(asteroidsGrabbedList.Count < 1)
				asteroidGrabber.transform.position = Vector3.Lerp(asteroidGrabber.transform.position,m_position, Time.deltaTime);
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
		if(used && asteroidsGrabbedList.Count < 1)  {
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 350.0f, 40.0f), "Use ASWD to move the gravity field");
		}
		else if(used && asteroidsGrabbedList.Count > 0)
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 400.0f, 40.0f), "Press ASDW to add force in that direction and release space!!");
	}
}
