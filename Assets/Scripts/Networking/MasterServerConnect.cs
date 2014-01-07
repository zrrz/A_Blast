using UnityEngine;
using System.Collections;

public class MasterServerConnect : MonoBehaviour {

	public string gameName = "It's A Blast!";
	public int serverPort = 25002;

	float timeoutHostList = 0.0f;
	float lastHostListRequest = -1000.0f;
	float hostListRefreshTimeout = 10.0f;

	ConnectionTesterStatus connectionTestResults = ConnectionTesterStatus.Undetermined;
	bool filterNATHosts = false;
	bool probingPublicIP = false;
	bool doneTesting = false;
	float timer = 0.0f;
	bool useNat = false;

	Rect windowRect;
	Rect serverListRect;
	bool hideTest = false;
	string testMessage = "Undetermined NAT capabilites";
	
	void Awake () {
		DontDestroyOnLoad (this);
		//If not running a client on the server machine
		//MasterServer.dedicatedServer = true;

		windowRect = new Rect (Screen.width - 350.0f, 0.0f, 350.0f, 100.0f);
		serverListRect = new Rect (0.0f, 0.0f, Screen.width - windowRect.width, 100.0f);

		//connectionTestResults = Network.TestConnection ();

		if (Network.HavePublicAddress ())
			Debug.Log ("Has public IP address");
		else
			Debug.Log ("Has private IP address");
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info) {
		Debug.Log (info);
	}

	void OnFailedToConnect(NetworkConnectionError info) {
		Debug.Log (info);
	}

	void Update () {
		if (!doneTesting)
			TestConnection ();
	}

	void TestConnection() {
		connectionTestResults = Network.TestConnection ();
		switch (connectionTestResults) {
			case ConnectionTesterStatus.Error:
				testMessage = "Problem determining NAT capabilites";
				doneTesting = true;
				break;
			case ConnectionTesterStatus.Undetermined:
				testMessage = "Undetermined NAT capabilites";
				doneTesting = false;
				break;
			case ConnectionTesterStatus.PublicIPIsConnectable:
				testMessage = "Directly connectable public IP address";
				useNat = false;
				doneTesting = true;
				break;
			//Find out if we can NAT punchthrough
			case ConnectionTesterStatus.PublicIPPortBlocked:
				testMessage = "Non-connectable public ip address (port " + serverPort + " blocked), running a server is impossible.";
				useNat = false;
				if(!probingPublicIP) {
					Debug.Log("Testing if firewall can be circumvented");
					connectionTestResults = Network.TestConnection();
					probingPublicIP = true;
					timer = Time.time + 10.0f;
				} else if(Time.time > timer) {
					probingPublicIP = false;
					useNat = true;
					doneTesting = true;
				}
				break;
			case ConnectionTesterStatus.PublicIPNoServerStarted:
				testMessage = "Public IP address but server not initialized. Must be a server to check server availability. Restart connection test when ready.";
				break;
			case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
				Debug.Log(connectionTestResults);
				testMessage = "Limited Nat punchthrough capabilites. Cannot connect to all types of NAT servers.";
				useNat = true;
				doneTesting = true;
				break;
			case ConnectionTesterStatus.LimitedNATPunchthroughSymmetric:
				Debug.Log(connectionTestResults);
				testMessage = "Limited NAT punchthrough capabilites. Cannot connect to all types of Nat servers. Running a server is ill advised as not everyone can connect.";
				useNat = true;
				doneTesting = true;
				break;
			case ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone:
			case ConnectionTesterStatus.NATpunchthroughFullCone:
				Debug.Log("NATpunchthroughAddressRestrictedCone || NATpunchthroughFullCone");
				testMessage = "Nat punchthrough capable. Can connect to all servers and recieve connections from all clients. Enabling Nat punchthrough capability.";
				useNat = true;
				doneTesting = true;
				break;
		default: 
			testMessage = "Error in test routine, got " + connectionTestResults;
			break;
		}	
	}

	void MakeWindow(int id) {
		hideTest = GUILayout.Toggle (hideTest, "Hide test info");

		if (!hideTest) {
			GUILayout.Label(testMessage);
			if(GUILayout.Button("Retest connection")) {
				Debug.Log("Redoing connection test");
				probingPublicIP = false;
				doneTesting = false;
				connectionTestResults = Network.TestConnection(true);
			}
		}

		if (Network.peerType == NetworkPeerType.Disconnected) {
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10.0f);
			if (GUILayout.Button ("Start Server")) {
				Network.InitializeServer (4, serverPort, useNat);
				MasterServer.RegisterHost (gameName, "game", "game test");
			}

			//Refresh hosts
			if (GUILayout.Button ("Refresh available Servers") || Time.realtimeSinceStartup > lastHostListRequest + hostListRefreshTimeout) {
				MasterServer.RequestHostList (gameName);
				lastHostListRequest = Time.realtimeSinceStartup;
			}

			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		} else {
			if(GUILayout.Button("Disconnect")) {
				Network.Disconnect();
				MasterServer.UnregisterHost();
			}
			GUILayout.FlexibleSpace();
		}
		GUI.DragWindow( new Rect(0.0f, 0.0f, 1000.0f, 1000.0f));
	}

	void MakeClientWindow(int id) {
			GUILayout.Space (5.0f);

			HostData[] data = MasterServer.PollHostList();
			int count = 0;
			foreach (HostData element in data) {
				GUILayout.BeginHorizontal();

				if( !(filterNATHosts && element.useNat) ) {
					string connections = element.connectedPlayers + "/" + element.playerLimit;
					GUILayout.Label(element.gameName);
					GUILayout.Label(element.gameName);
					GUILayout.Space(5.0f);
					GUILayout.Label(connections);
					GUILayout.Space(5.0f);
					string hostInfo = "";

					if(element.useNat) {
						GUILayout.Label("NAT");
						GUILayout.Space(5.0f);
					}

					//All IP adrresses. Should show just first one to user. Internally Unity connects to them all until a connection is made
					foreach(string host in element.ip) {
						hostInfo = hostInfo + host + ":" + element.port + " ";
					}

					GUILayout.Label(hostInfo);
					GUILayout.Space(5.0f);
					GUILayout.Label(element.comment);
					GUILayout.Space(5.0f);
					GUILayout.FlexibleSpace();
					if(GUILayout.Button("Connect"))
						Network.Connect(element);
				}
				GUILayout.EndHorizontal();
			}
	}

	void OnGUI() {
		windowRect = GUILayout.Window (0, windowRect, MakeWindow, "Server Controls");
		if (Network.peerType == NetworkPeerType.Disconnected && MasterServer.PollHostList ().Length != 0)
			serverListRect = GUILayout.Window (1, serverListRect, MakeClientWindow, "Server List");
	}
}
