using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AuthorativeSpawn : MonoBehaviour {
	
	public Transform playerPrefab;

	NetworkPlayer localPlayer;
	NetworkViewID localTransformViewID;
	NetworkViewID localAnimationViewID;
	bool isInstantiated = false;

	public Transform[] spawnPoints;

	List<PlayerInfo> playerInfo = new List<PlayerInfo>();

	class PlayerInfo {
		public NetworkViewID transformViewID;
		public NetworkViewID animationViewID;
		public NetworkPlayer player;
	}

	void OnGUI() {
		if (Network.isClient && int.Parse(localPlayer.ToString()) != 0 && !isInstantiated) {
			if(GUI.Button(new Rect(20.0f, Screen.height-60.0f, 90.0f, 20.0f), "SpawnPlayer")) {
				networkView.RPC("SpawnPlayer", RPCMode.AllBuffered, localPlayer, localTransformViewID, localAnimationViewID);
				isInstantiated = true;
			}
		}
	}

	//Receive server initialization, record own identifier as seen by the server
	//This is later used to recognize if a network spawned player is the local player
	//Also record assigned view IDs so the server can synch the player correctly
	[RPC]
	void InitPlayer(NetworkPlayer player, NetworkViewID tViewID, NetworkViewID aViewID) {
		Debug.Log ("Recieved player init " + player + " . ViewIDs " + tViewID + " and " + aViewID);
		localPlayer = player;
		localTransformViewID = tViewID;
		localAnimationViewID = aViewID;
	}

	//Create a networked player in the game. Instantiate a local cope of the player, set the view IDs accordingly
	[RPC]
	void SpawnPlayer(NetworkPlayer playerIdentifier, NetworkViewID transformViewID, NetworkViewID animationViewID) {
		Debug.Log ("Instantiating player " + playerIdentifier);
		Transform instantiatedPlayer = (Transform)Instantiate (playerPrefab, transform.position, transform.rotation);
		NetworkView[] networkViews = instantiatedPlayer.GetComponents<NetworkView>();

		//Assign view IDs to player object
		if (networkViews.Length != 2) {
			Debug.Log("Error while spawning player, prefab should have 2 network views, has " + networkViews.Length);
		} else {
			networkViews[0].viewID = transformViewID;
			networkViews[1].viewID = animationViewID;
		}
		//Initialize local player
		if (playerIdentifier == localPlayer) {
			Debug.Log("Enabling user input as this is the local player");
			//We are doing client prediction, so we enable input
			instantiatedPlayer.GetComponent<PlayerInput>().enabled = true;
			instantiatedPlayer.GetComponent<PlayerInput>().getUserInput = true;
			//Enable input network synchronization (server gets input)
			//instantiatedPlayer.GetComponent<NetworkController>().enabled = true;
			instantiatedPlayer.SendMessage("SetOwnership", playerIdentifier);
			return;
		} else if(Network.isServer) {
			instantiatedPlayer.GetComponent<PlayerInput>().enabled = true;
			//instantiatedPlayer.GetComponent<AuthServerPlayerAnimation>().enabled = true;
			//Record player info so he can be destroyed properly
			PlayerInfo playerInstance = new PlayerInfo();
			playerInstance.transformViewID = transformViewID;
			playerInstance.animationViewID = animationViewID;
			playerInstance.player = playerIdentifier;
			playerInfo.Add(playerInstance);
			Debug.Log("There are now" + playerInfo.Count + " players active");
		}
	}

	//This runs if the scene is executed from the loader scene.
	//Here we must check if we already have clients connect which must be reanitialized.
	//This is the same procedure as in OnPlayerConnected except we process already
	//connected players instead of the new ones. The already connected players also
	//reloaded the level and this have a clean slate.
	void OnNetworkLevelLoaded() {
		if (Network.isServer && Network.connections.Length > 0) {
			foreach(NetworkPlayer player in Network.connections) {
				Debug.Log("Resending player init to " + player);
				NetworkViewID transformViewID = Network.AllocateViewID();
				NetworkViewID animationViewID = Network.AllocateViewID();
				Debug.Log("Player given view IDs " + transformViewID + " and " + animationViewID);
				networkView.RPC("InitPlayer", player, player, transformViewID, animationViewID);
			}
		}
	}

	//Send initalization info to the new player, before that he cannot spawn himself
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("sending player init to " + player);
		NetworkViewID transformViewID = Network.AllocateViewID();
		NetworkViewID animationViewID = Network.AllocateViewID();
		Debug.Log("Player given view IDs " + transformViewID + " and " + animationViewID);
		networkView.RPC("InitPlayer", player, player, transformViewID, animationViewID);
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log ("Cleaning up player " + player);
		//Destroy the player object this network player spawned
		PlayerInfo deletePlayer = null;
		foreach (PlayerInfo playerInstance in playerInfo) {
			if(player == playerInstance.player) {
				Debug.Log("Destroying objects belonging to view ID " + playerInstance.transformViewID);
				Network.Destroy(playerInstance.transformViewID);
				deletePlayer = playerInstance;
			}
		}
		if (null != deletePlayer) {
			playerInfo.Remove (deletePlayer);
			Network.RemoveRPCs (player, 0);
			Network.DestroyPlayerObjects (player);
		}
	}

	//void Start() {
	//	Network.Instantiate(playerPrefab, spawnPoints[0].position, transform.rotation, 0);
	//}

	//void OnDisconnectedFromServer(NetworkDisconnection info) {
	//	Application.LoadLevel("Server");
	//}
}