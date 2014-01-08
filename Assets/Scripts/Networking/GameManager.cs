using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform spawnPos;
	public GameObject playerPrefab;

	void Start () {
		SpawnPlayers();
	}
	
	void Update () {
	
	}

	void SpawnPlayers () {

		NewNetwork gameNetwork = GameObject.FindGameObjectWithTag("NewNetwork").GetComponent<NewNetwork>();

		GameObject newPlayer = Network.Instantiate (playerPrefab, spawnPos.position, Quaternion.identity, 0) as GameObject;
			
		newPlayer.GetComponent<Player>().networkView.RPC("SetPlayer", RPCMode.AllBuffered, gameNetwork.playerName);	
		print ("Spawned");

		
	}

}
