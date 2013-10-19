using UnityEngine;
using System.Collections;

public class TestPlayerNetwork : MonoBehaviour {

	public Transform cubePrefab;
	
	void OnServerIntialized() {
		SpawnPlayer();
	}
	
	void OnConnectedToServer() {
		SpawnPlayer();
	}
	
	void SpawnPlayer() {
		Transform myTransform = (Transform)Network.Instantiate(cubePrefab, transform.position, transform.rotation, 0);
	}
}