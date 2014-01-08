using UnityEngine;
using System.Collections;

public class PlayerInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(networkView.isMine) {
			GetComponent<PlayerInput>().enabled = true;
			GetComponent<PlayerMove>().enabled = true;
			Camera.main.GetComponent<CameraFollow>().player = transform;
			Camera.main.GetComponent<CameraFollow>().Reset();
		}else{
			GetComponent<PlayerInput>().enabled = false;
			GetComponent<PlayerMove>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
