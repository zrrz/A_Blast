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
	public float inflation = 1.0f;
	public float minInflation = 1.0f;
	public float maxInflation = 3.0f;
	
	public float moveRadius = 30.0f;

	public float forceIncreaseAmount = 20.0f;

	CameraFollow cameraFollow;
	
	public List<GameObject> asteroidsGrabbedList;
	public GameObject asteroidGrabber;

	Transform shipCenter;

	public float maxGrabberDistance = 60.0f;

	void Start () {
		cameraFollow = Camera.main.GetComponent<CameraFollow>();
		player = GameObject.Find("Player").GetComponent<PlayerMove>();
		input = player.GetComponent<PlayerInput>();
		asteroidGrabber.GetComponent<AsteroidGrabber>();
		m_position = asteroidGrabber.transform.position;
	
		shipCenter = GameObject.FindGameObjectWithTag ("PlayerShip").transform;
	}

	void Update () {
		if(playerNear) {
			if(Input.GetKeyDown(KeyCode.E)) {
				used = !used;
				player.usingDevice = !player.usingDevice;
				if(used)
				{
					cameraFollow.ChangeCam(camAnchor);
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
				Vector3 dirToAdd = Vector3.zero;
				if(Input.GetKey(KeyCode.A))
				{
					dirToAdd += new Vector3(-4,0,0);
				}
				if(Input.GetKey(KeyCode.D))
				{
					dirToAdd += new Vector3(4,0,0);
				}
				if(Input.GetKey(KeyCode.W))
				{
					dirToAdd += new Vector3(0,0,4);
				}
				if(Input.GetKey(KeyCode.S))
				{
					dirToAdd += new Vector3(0,0,-4);
				}
				if(Vector3.Distance(asteroidGrabber.gameObject.transform.position + dirToAdd, shipCenter.position) < maxGrabberDistance) {
					m_position += dirToAdd;
				}
				if(Input.GetKeyDown(KeyCode.N))
				{
					asteroidGrabber.GetComponent<AsteroidTest>().IncreaseScale(1.0f);
				}
				if(Input.GetKeyDown(KeyCode.Space))
				{
					Collider[] collisions = Physics.OverlapSphere(asteroidGrabber.transform.position, ((SphereCollider)asteroidGrabber.collider).radius * asteroidGrabber.transform.localScale.x + 1);
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
				
				if(Input.GetKey(KeyCode.A))
				{
					newforce += new Vector3(-forceIncreaseAmount,0,0);
					Debug.Log ("adding directional forces: " + newforce);
				}
				if(Input.GetKey(KeyCode.D))
				{
					newforce += new Vector3(forceIncreaseAmount,0,0);
					Debug.Log ("adding directional forces: " + newforce);
				}
				if(Input.GetKey(KeyCode.W))
				{
					newforce += new Vector3(0,0,forceIncreaseAmount);
					Debug.Log ("adding directional forces: " + newforce);
				}
				if(Input.GetKey(KeyCode.S))
				{
					newforce += new Vector3(0,0,-forceIncreaseAmount);
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
		/*
		if(inflation > 1.0f)
			inflation -= Time.deltaTime;
		else
			Mathf.Clamp(inflation, minInflation, maxInflation);
			*/
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
			GUI.Box(new Rect(0.0f, Screen.height - 80.0f, 350.0f, 40.0f), "Use ASWD to move the gravity field");
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 350.0f, 40.0f), "Press N to increase the radius for " +  " energy");
		}
		else if(used && asteroidsGrabbedList.Count > 0)
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 400.0f, 40.0f), "Press ASDW to add force in that direction and release space!!");
	}
}
