using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	
	BaseInput input;
	
	//Transform thisTransform;
	
	public bool UsingDevice = false;
	
	public float moveSpeed = 5.0f;
	
	CharacterController characterController;
	
	public float sprintSpeed = 2.5f;
			
	void Start () {
		//thisTransform = transform;
		input = GetComponent<BaseInput>();
		characterController = GetComponent<CharacterController>();
	}
	
	void Update () {
		if(!UsingDevice) {
			float speedMod = (input.shift == false) ? 1.0f : sprintSpeed;
			characterController.Move(input.dir * (moveSpeed + speedMod) * Time.deltaTime);
			//thisTransform.position += input.dir * moveSpeed * Time.deltaTime;
		}
	}
	
	void OnGUI() {
		if(!UsingDevice)  {
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 150.0f, 40.0f), "Press 'shift' to sprint");
		}
	}
}
