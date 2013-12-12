using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int m_health = 100;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TakeDamage(float damage) {
		m_health -= (int)damage;
	}
}
