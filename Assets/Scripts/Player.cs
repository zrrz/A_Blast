using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string playerName;

	public GameObject nameText;

	void Start () {
	
	}
	
	void Update () {
	
	}

	[RPC]
	void SetPlayer (string newName){

		playerName = newName;
		nameText.GetComponent<TextMesh>().text = playerName;

	}

}
