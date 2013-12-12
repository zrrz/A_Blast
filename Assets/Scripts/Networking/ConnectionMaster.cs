using UnityEngine;
using System.Collections;

public class ConnectionMaster : MonoBehaviour {

	void Start() {
		DontDestroyOnLoad (gameObject);
	}

	public int connectionPort = 25001;
	
	void OnGUI() {
		if(Network.peerType == NetworkPeerType.Disconnected) {
			GUI.Label(new Rect(10.0f, 10.0f, 200.0f, 20.0f), "Status: Disconnected");
			string ip = GUI.TextArea(new Rect(10.0f, 30.0f, 100.0f, 20.0f), "127.0.0.1");
			if(GUI.Button(new Rect(10.0f, 50.0f, 120.0f, 20.0f), "Client Connect")) {
				Network.Connect(ip, connectionPort);
				Application.LoadLevel("Main");
			}
			if(GUI.Button(new Rect(10.0f, 70.0f, 120.0f, 20.0f), "Initialize Server")) {
				Network.InitializeServer(32, connectionPort, false);
				Application.LoadLevel("Main");
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
