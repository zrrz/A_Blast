using UnityEngine;
using System.Collections;

public class CubeMoveAuthoritative : MonoBehaviour {
	
	public NetworkPlayer theOwner;
	float lastClientHInput = 0.0f;
	float lastClientVInput = 0.0f;
	float serverCurrentHInput = 0.0f;
	float serverCurrentVInput = 0.0f;
	
	void Awake() {
		if(Network.isClient) {
			enabled = false;
		}
	}
	
	[RPC]
	void SetPlayer(NetworkPlayer player) {
		theOwner = player;
		if(player == Network.player)
			enabled = true;
	}
	
	void Update() {
		if(theOwner != null && Network.player == theOwner) {
			float hInput = Input.GetAxis("Horizontal");
			float vInput = Input.GetAxis("Vertical");
			
			if(lastClientHInput != hInput || lastClientVInput != vInput) {
				lastClientHInput = hInput;
				lastClientVInput = vInput;
			}
			
			if(Network.isServer) {
				SendMovementInput(hInput, vInput);
			} else if(Network.isClient) {
				networkView.RPC("SendMovementInput", RPCMode.Server, lastClientHInput, lastClientVInput);
			}
		}
		
		if(Network.isServer) {
			Vector3 moveDirection = new Vector3(serverCurrentHInput, 0.0f, serverCurrentVInput);
			float speed = 5.0f;
			transform.Translate(speed * moveDirection * Time.deltaTime);
		}
	}
	
	[RPC]
	void SendMovementInput(float hInput, float vInput) {
		serverCurrentHInput = hInput;
		serverCurrentVInput = vInput;
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		if(stream.isWriting) {
			Vector3 pos = transform.position;
			stream.Serialize(ref pos);
		} else {
			Vector3 posRecieve = Vector3.zero;
			stream.Serialize(ref posRecieve);
			transform.position = posRecieve;
		}
	}
}
