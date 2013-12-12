using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AuthorativeSpawn : MonoBehaviour {
	
	public Transform playerPrefab;
	public List<CubeMoveAuthoritative> playerScripts = new List<CubeMoveAuthoritative>();

	public Transform[] spawnPoints;

	void OnDisconnectedFromServer(NetworkDisconnection info) {
			Application.LoadLevel("Server");
	}
	
	void OnLevelWasLoaded(int level) {

		SpawnPlayer (Network.player);
	}
	
	void SpawnPlayer(NetworkPlayer player) {
		string tempPlayerString = player.ToString();
		int playerNumber =  Convert.ToInt32(tempPlayerString);

		Transform newPlayerTransform = null;
		if(Network.isServer) {
			newPlayerTransform = (Transform)Network.Instantiate(playerPrefab, spawnPoints[0].position, transform.rotation, playerNumber);
		} else if(Network.isClient) {
			newPlayerTransform = (Transform)Network.Instantiate(playerPrefab, spawnPoints[1].position, transform.rotation, playerNumber);
		}
		//playerScripts.Add(newPlayerTransform.GetComponent<CubeMoveAuthoritative>());
		if(!networkView.isMine) {
			newPlayerTransform.gameObject.GetComponent<PlayerMove>().enabled = false;
			newPlayerTransform.gameObject.GetComponent<PlayerInput>().enabled = false;
		}
	
		NetworkView theNetworkView = newPlayerTransform.networkView;
		//theNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
	}
}
