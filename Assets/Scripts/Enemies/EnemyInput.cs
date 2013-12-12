using UnityEngine;
using System.Collections;

public class EnemyInput : BaseInput {

	GameObject playerShip;

	public float shootDistance = 10.0f;

	void Start () {
		playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
	}

	void Update () {
		_dir = Vector3.zero;
		if (Vector3.Distance (transform.position, playerShip.transform.position) > shootDistance) {
			_dir = (playerShip.transform.position - transform.position).normalized;
		} else {
			_targetDir = (playerShip.transform.position - transform.position).normalized;
			_fire = true;
		}
	}
}
