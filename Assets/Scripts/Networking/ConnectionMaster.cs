using UnityEngine;
using System.Collections;

public class ConnectionMaster : MonoBehaviour {
	public int connectionPort = 25001;

	string ip = "127.0.0.1";

	private int lastLevelPrefix = 0;

	void Start() {
		networkView.group = 1;
		DontDestroyOnLoad (gameObject);
		Application.LoadLevel("Server");
	}

	[RPC]
	IEnumerator LoadLevel(string level, int levelPrefix) {
		lastLevelPrefix = levelPrefix;

		Network.SetSendingEnabled(0, false);

		Network.isMessageQueueRunning = false;

		Network.SetLevelPrefix(levelPrefix);
		Application.LoadLevel(level);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();

		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled(0, true);

		foreach(GameObject obj in FindObjectsOfType(typeof(GameObject))) {
			obj.SendMessage("OnNetworkLevelLoaded", SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnDisconnectedFromServer() {
		Application.LoadLevel("Server");
	}

	void OnGUI() {
		if(Network.peerType == NetworkPeerType.Disconnected) {
			GUI.Label(new Rect(10.0f, 10.0f, 200.0f, 20.0f), "Status: Disconnected");
			ip = GUI.TextArea(new Rect(10.0f, 30.0f, 100.0f, 20.0f), ip);
			if(GUI.Button(new Rect(10.0f, 50.0f, 120.0f, 20.0f), "Client Connect")) {
				Network.Connect(ip, connectionPort);
			}
			if(GUI.Button(new Rect(10.0f, 70.0f, 120.0f, 20.0f), "Initialize Server")) {
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
			if(Application.loadedLevel == 1) {
				if(GUI.Button(new Rect(10.0f, 50.0f, 120.0f, 20.0f), "Start")) {
					Network.RemoveRPCsInGroup(0);
					Network.RemoveRPCsInGroup(1);
					networkView.RPC("LoadLevel", RPCMode.AllBuffered, "Main", lastLevelPrefix+1);
					//StartCoroutine(LoadLevel("Main", lastLevelPrefix+1));
				}
			}
		}
	} 
}
