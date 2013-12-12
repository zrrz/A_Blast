using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldBar : MonoBehaviour {
	public List<GameObject> currList;

	ShieldConsole console; 

	void Start () {
		console = GameObject.Find ("ShieldConsole").GetComponent<ShieldConsole>();
	}

	void Update () {
	
	}

	void TakeDamage(float damage) {
		console.RemoveBar(currList);
	}
}
