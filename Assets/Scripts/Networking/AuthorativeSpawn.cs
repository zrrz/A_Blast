using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AuthorativeSpawn : MonoBehaviour {
	
	public Transform playerPrefab;
	public List<CubeMoveAuthoritative> playerScripts = new List<CubeMoveAuthoritative>();

	public Transform[] spawnPoints;
	
	//void OnServerInitialized() {
	//	SpawnPlayer(Network.player);
	//}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		//if(Network.isServer) {
			Application.LoadLevel("Server");
		//} else {
			
		//}
	}
	
	//void OnPlayerConnected(NetworkPlayer player) {
	//	SpawnPlayer(player);
	//}

	void OnLevelWasLoaded(int level) {
		SpawnPlayer (Network.player);
	}
	
	void SpawnPlayer(NetworkPlayer player) {
		string tempPlayerString = player.ToString();
		int playerNumber =  Convert.ToInt32(tempPlayerString);

		Transform newPlayerTransform = (Transform)Network.Instantiate(playerPrefab, spawnPoints[playerNumber].position, transform.rotation, playerNumber);
		playerScripts.Add(newPlayerTransform.GetComponent<CubeMoveAuthoritative>());	
	
		NetworkView theNetworkView = newPlayerTransform.networkView;
		//theNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
	}
}
