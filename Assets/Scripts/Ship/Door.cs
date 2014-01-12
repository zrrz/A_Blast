using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	GameObject doorL, doorR;

	bool playerNear = false;

	bool open = false;

	bool moving = false;

	void Start () {
		doorL = transform.FindChild ("DoorL").gameObject;
		doorR = transform.FindChild ("DoorR").gameObject;
	}

	void Update () {
		if (playerNear) {
			if (!open) {
				if (!moving) {
					moving = true;
					animation.Play ("DoorOpen");
				} else {
					if (!animation.isPlaying) {
						open = true;
						moving = false;
					}
				}
			}
		} else {
			if(open) {
				if(!moving) {
					moving = true;
					animation.Play("DoorClose");
				} else {
					if (!animation.isPlaying) {
						open = false;
						moving = false;
					}
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
}
