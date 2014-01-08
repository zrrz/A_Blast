/* Network manager:
 * Create games
 * Join games
 * Start game
 * Manages current players in room
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewNetwork : MonoBehaviour {

	public string playerName;
	//private int myPlayer;

	public string gameName = "It's A Blast!";
	public int maxPlayers = 12;

	public int serverPort = 25002;
	bool useNat = false;


	private HostData currentRoom;
	public List<PlayerLobby> players = new List<PlayerLobby>();


	void Start () {
		DontDestroyOnLoad(this);  
	}

	private void NewGame() {

		gameName = GUILayout.TextField (gameName, 25);

		if (GUILayout.Button ("New Game")) {
			players.Clear();
			Network.InitializeServer (maxPlayers, serverPort, useNat);
			MasterServer.RegisterHost ("test", gameName, "for testing");
			print ("New game");

		}

	}

	void ShowServers (){
		
		HostData[] hostList = MasterServer.PollHostList(); // Get current room list


		GUILayout.Box ("Rooms: " + hostList.Length);

		for (int h = 0; h < hostList.Length; h++) {
			
			GUILayout.Box (hostList[h].gameName + " players: " + hostList[h].connectedPlayers + "/" + hostList[h].playerLimit);
			if (GUILayout.Button ("Join")){
				players.Clear();
				Network.Connect (hostList[h]);
				currentRoom = hostList[h];
			}
			
		}

		if (GUILayout.Button ("Refresh")){
			MasterServer.ClearHostList(); 
			MasterServer.RequestHostList("test"); 
		}

	}

	void ShowRoomInfo (){

		GUILayout.Box ("Players: " + Network.connections.Length);
		for (int p=0; p<players.Count; p++){
			GUILayout.Box (p+1 + ". " + players[p].name);	
		}

		if (GUILayout.Button ("Start"))
			networkView.RPC("StartGame", RPCMode.AllBuffered);

		if (GUILayout.Button ("Leave"))
			LeaveRoom();

	}


	void OnServerInitialized(){
		networkView.RPC("AddPlayer", RPCMode.AllBuffered, playerName, Network.player);	
	}

	void OnPlayerConnected () {
		//networkView.RPC("AddPlayer", RPCMode.AllBuffered, playerName, Network.player);	
	}

	void OnConnectedToServer () {
		networkView.RPC("AddPlayer", RPCMode.AllBuffered, playerName, Network.player);	
	}

	[RPC]
	void AddPlayer (string newName, NetworkPlayer netPlayer){

		PlayerLobby newPlayer = new PlayerLobby();
		newPlayer.name = newName;
		newPlayer.net = netPlayer;
		
		players.Add (newPlayer);
		print ("New player joined");

	}



	// Cleanup
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " +  player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		RemovePlayer(player);
	}

	void LeaveRoom (){
		Network.Disconnect();
		players.Clear();
	}

	void RemovePlayer (NetworkPlayer player){
		for (int p=0; p<players.Count; p++){
			if (player == players[p].net){
				players.RemoveAt(p);
				break;
			}
		}
	}

	[RPC]
	void StartGame (){

		Application.LoadLevel("Main");

	}
	

	void OnGUI (){

		GUILayout.BeginArea (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 150, 400, 300));
		if (Application.loadedLevelName == "PreServer"){

			if (Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server) 
				ShowRoomInfo();
			else {
				ShowServers();
				GUILayout.Space(10);
				NewGame();
				GUILayout.Space(10);
				playerName = GUILayout.TextField (playerName, 25);
			}
		
		}
		GUILayout.EndArea();

	}

}

public class PlayerLobby {
	
	public string name;
	public NetworkPlayer net;
	

}


