using UnityEngine;
using System.Collections;

public class GameOverZone : MonoBehaviour {	
	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "PlayerShip") {
			Application.LoadLevel("Win");
		}
	}
}
