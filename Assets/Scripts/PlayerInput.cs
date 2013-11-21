using UnityEngine;
using System.Collections;

public class PlayerInput : BaseInput {

	void Start () {
	
	}

	void Update () {
		_dir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")); 
		_button1 = Input.GetButton("Fire1");
		_shift = Input.GetKey(KeyCode.LeftShift);
	}
}
