using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject ship;
	public GameObject shipSpawn;
	public Transform shipSpawnPos;

	public Transform spawnPos;
	public GameObject playerPrefab;

	void Start () {
		if (Network.isServer)
			ship = Network.Instantiate (shipSpawn, shipSpawnPos.position, Quaternion.identity, 0) as GameObject;
		SpawnPlayers();
	}

	void SpawnPlayers () {

		NewNetwork gameNetwork = GameObject.FindGameObjectWithTag("NewNetwork").GetComponent<NewNetwork>();

		GameObject newPlayer = Network.Instantiate (playerPrefab, spawnPos.position, Quaternion.identity, 0) as GameObject;
		newPlayer.transform.parent = ship.transform;	

		newPlayer.GetComponent<Player>().networkView.RPC("SetPlayer", RPCMode.AllBuffered, gameNetwork.playerName);	
		print ("Spawned");

		
	}

}
