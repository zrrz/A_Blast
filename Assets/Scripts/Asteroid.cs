using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	public GameObject boundsMax;
	public GameObject boundsMin;
	
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
}
