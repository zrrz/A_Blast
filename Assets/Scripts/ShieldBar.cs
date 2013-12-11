using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldBar : MonoBehaviour {
	public List<GameObject> currList;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Asteroid")
		{
			Debug.Log ("hit shield " + gameObject.name);
			collision.gameObject.SetActive(false);
			gameObject.transform.parent.GetComponent<Shields>().RemoveBar(currList);
		}
	}
}
