using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldConsole : MonoBehaviour {
	
	BaseInput input;	
	PlayerMove player;
	
	bool used = false;
	bool playerNear = false;
	
	public float camSize = 60.0f;
	public Transform camAnchor;
	
	CameraFollow cameraFollow;

	public List<GameObject> ShieldBarList;
	public List<GameObject> LeftBarList;
	public List<GameObject> RightBarList;
	public List<GameObject> TopBarList;
	public List<GameObject> BottomBarList;
	public Vector3 m_position;
	public GameObject[] tempBarList;
	
	public int freeBars = 0;
	
	public int tempIndex;
	
	public int totalBars = 12;

	void Start () {		
		cameraFollow = Camera.main.GetComponent<CameraFollow>();
		player = GameObject.Find("Player").GetComponent<PlayerMove>();
		input = player.GetComponent<PlayerInput>();

		tempBarList = GameObject.FindGameObjectsWithTag("ShieldBar");
		m_position = transform.position;
		
		foreach(GameObject bar in RightBarList)
			bar.GetComponent<ShieldBar>().currList = RightBarList;
		
		foreach(GameObject bar in LeftBarList)
			bar.GetComponent<ShieldBar>().currList = LeftBarList;
		
		foreach(GameObject bar in TopBarList)
			bar.GetComponent<ShieldBar>().currList = TopBarList;
		
		foreach(GameObject bar in BottomBarList)
			bar.GetComponent<ShieldBar>().currList = BottomBarList;
	}

	void Update () {
		if(playerNear) {
			if(Input.GetKeyDown(KeyCode.E)) {
				used = !used;
				player.usingDevice = !player.usingDevice;
				if(used)
				{
					cameraFollow.ChangeCam(camAnchor);
					Debug.Log ("Shield Console Entered");
				}
				else
				{
					cameraFollow.Reset();
					Debug.Log ("Shield Console Exited");
				}
			}
		}
		if(used) {
			if(!Input.GetKey(KeyCode.Space)) {
				if(Input.GetKeyDown(KeyCode.RightArrow))
					AddBar(RightBarList);
				else if(Input.GetKeyDown(KeyCode.LeftArrow))
					AddBar(LeftBarList);
				else if(Input.GetKeyDown(KeyCode.UpArrow))
					AddBar(TopBarList);
				else if(Input.GetKeyDown(KeyCode.DownArrow))
					AddBar(BottomBarList);
			}
			
			if(Input.GetKey(KeyCode.Space)) {
				if(Input.GetKeyDown(KeyCode.RightArrow))
					RemoveToFreeBar(RightBarList);
				else if(Input.GetKeyDown(KeyCode.LeftArrow))
					RemoveToFreeBar(LeftBarList);
				else if(Input.GetKeyDown(KeyCode.UpArrow))
					RemoveToFreeBar(TopBarList);
				else if(Input.GetKeyDown(KeyCode.DownArrow))
					RemoveToFreeBar(BottomBarList);
			}
		}
	}

	public int FindActiveIndex(List<GameObject> tlist) {
		int newIndex =0;
		foreach(GameObject bar in tlist)
		{
			if(bar.activeSelf == true)
				newIndex++;
		}	
		return newIndex -1;
	}
	
	void AddBar( List<GameObject> templist) {
		int index = FindActiveIndex(templist);
		index++;
		if(index < 3 && freeBars > 0)
		{
			templist[index].SetActive(true);
			freeBars--;
			totalBars++;
		}
	}
	
	public void RemoveBar( List<GameObject> templist) {
		int index = FindActiveIndex(templist);
		if(index > 0)
		{
			totalBars--;
			templist[index].SetActive(false);
			
		}
	}
	
	public void RemoveToFreeBar( List<GameObject> templist) {
		int index = FindActiveIndex(templist);
		if(index > 0)
		{
			freeBars++;
			totalBars--;
			templist[index].SetActive(false);
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
		GUI.Label(new Rect(40,40,50,200), "Total Shields left = " + totalBars);
		GUI.Label(new Rect(40,100,50,200), "Unused Bars = " + freeBars);

		if(playerNear) {
			GUI.Box(new Rect(0.0f, 0.0f, 150.0f, 50.0f), "Press 'E' to enter");
		}
		if(used)  {
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 150.0f, 40.0f), "Hold space and press an arrow key to remove a bar, press an arrow key to add a bar back");
		}
	}
}
