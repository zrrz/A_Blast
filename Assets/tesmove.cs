using UnityEngine;
using System.Collections;

public class tesmove : MonoBehaviour {
	
	Vector3 lastPosition;
	float minimumMovement = .05f;
	
	void Awake() {
		if(!networkView.isMine)
			enabled = false;
	}
	
	void Update () {
		if (networkView.isMine)
		{
		    Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		    float speed = 5;
		    transform.Translate(speed * moveDir * Time.deltaTime);
			//if(Vector3.Distance(transform.position, lastPosition) > minimumMovement) {
			//	lastPosition = transform.position;
			//	networkView.RPC("SetPosition", RPCMode.Others, transform.position);
			//}
		}
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		if(stream.isWriting) {
			Vector3 pos = transform.position;
			stream.Serialize(ref pos);
		} else {
			Vector3 recievedPosition = Vector3.zero;
			stream.Serialize(ref recievedPosition);
			transform.position = recievedPosition;
		}
	}
	
//	[RPC]
//	void SetPosition(Vector3 newPosition) {
//		transform.position = newPosition;
//	}
}