using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	public GameObject boundsMax;
	public GameObject boundsMin;

	public float damage = 20.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x <= boundsMin.transform.position.x)
		{
			gameObject.SetActive(false);
		}
		else if(transform.position.x >= boundsMax.transform.position.x)
		{
			gameObject.SetActive(false);
		}
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		collision.gameObject.SendMessage ("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		gameObject.SetActive(false);
	}
}
