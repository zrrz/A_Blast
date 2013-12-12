using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int m_health = 100;

	void Start () {
	
	}

	void Update () {
	
	}

	void TakeDamage(float damage) {
		m_health -= (int)damage;
	}
}
