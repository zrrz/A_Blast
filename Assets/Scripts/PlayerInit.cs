using UnityEngine;
using System.Collections;

public class PlayerInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(networkView.isMine) {
			GetComponent<PlayerInput>().enabled = true;
			GetComponent<PlayerMove>().enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
