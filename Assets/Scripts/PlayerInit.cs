using UnityEngine;
using System.Collections;

public class PlayerInit : MonoBehaviour {

	void Start () {
		if(networkView.isMine) {
			GetComponent<PlayerInput>().enabled = true;
			GetComponent<PlayerMove>().enabled = true;
			Camera.main.GetComponent<CameraFollow>().player = transform;
			Camera.main.GetComponent<CameraFollow>().Reset();
		}else{
			GetComponent<PlayerInput>().enabled = false;
			//GetComponent<PlayerMove>().enabled = false;
		}
	}
	
	void Update () {
	
	}
}
