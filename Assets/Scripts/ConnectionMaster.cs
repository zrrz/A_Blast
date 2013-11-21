using UnityEngine;
using System.Collections;

public class ConnectionMaster : MonoBehaviour {
	
	public string connectionIP = "127.0.0.1";
	public int connectionPort = 25001;
	
	void OnGUI() {
		if(Network.peerType == NetworkPeerType.Disconnected) {
			GUI.Label(new Rect(10.0f, 10.0f, 200.0f, 20.0f), "Status: Disconnected");
			if(GUI.Button(new Rect(10.0f, 30.0f, 120.0f, 20.0f), "Client Connect")) {
				Network.Connect(connectionIP, connectionPort);
			}
			if(GUI.Button(new Rect(10.0f, 50.0f, 120.0f, 20.0f), "Initialize Server")) {
				Network.InitializeServer(32, connectionPort, false);
			}
		} else if (Network.peerType == NetworkPeerType.Client) {
			GUI.Label(new Rect(10.0f, 10.0f, 300.0f, 20.0f), "Status: Connected as Client");
			if(GUI.Button(new Rect(10.0f, 30.0f, 120.0f, 20.0f), "Disconnect")) {
				Network.Disconnect(200);
			}
		} else if(Network.peerType == NetworkPeerType.Server) {
			GUI.Label(new Rect(10.0f, 10.0f, 300.0f, 20.0f), "Status: Connected as Server");
			if(GUI.Button(new Rect(10.0f, 30.0f, 120.0f, 20.0f), "Disconnect")) {
				Network.Disconnect(200);
			}
		}
	} 
}
