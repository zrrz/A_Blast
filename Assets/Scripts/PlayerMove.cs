using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	
	BaseInput input;
	
	public bool usingDevice = false;
	
	public float moveSpeed = 5.0f;
	public float sprintSpeed = 2.5f;

	CharacterController characterController;
	
	public Vector3 movePos;
			
	void Start () {
		input = GetComponent<BaseInput>();
		characterController = GetComponent<CharacterController>();
		movePos = transform.position;
	}
	
	void FixedUpdate () {
		if(!usingDevice && networkView.isMine) {
			float speedMod = (input.shift == false) ? 1.0f : sprintSpeed;
			characterController.SimpleMove(input.dir * (moveSpeed + speedMod) * Time.deltaTime);
		}

		if(networkView.isMine == false){
			transform.position = Vector3.Lerp (transform.position, movePos, 0.175f);
			print ("following");
		}
	}

	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){

		if (stream.isWriting){
			Vector3 pos = transform.position;
			stream.Serialize(ref pos);
		}else{

			Vector3 posRec = transform.position;
			stream.Serialize(ref posRec);

			movePos = posRec;

		}


	}
	
	void OnGUI() {
		if(!usingDevice)  {
			GUI.Box(new Rect(0.0f, Screen.height - 40.0f, 150.0f, 40.0f), "Press 'shift' to sprint");
		}
	}
}
