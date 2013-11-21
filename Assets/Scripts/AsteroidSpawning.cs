using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawning : MonoBehaviour {
	
	public GameObject leftBound;
	public GameObject rightBound;
	public GameObject startBound;
	public GameObject endBound;
	public List<GameObject> asteroidList;
	public GameObject currentAsteroid;
	
	public float asteroidSpeedMin;
	public float asteroidSpeedMax;
	
	// Use this for initialization
	void Start () {
		
		GameObject[] tempAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
		
		foreach(GameObject asteroid in tempAsteroids)
		{
			asteroidList.Add (asteroid);
			asteroid.SetActive(false);
		}
		
		FireAsteroid();
		//StartCoroutine("FireAsteroid");
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
	
	Vector3 GetRandomFirePos()
	{
		Vector3 fireVec;
		GameObject CurrSide;
		float tempX;
		float tempZ;
		
		if(Time.time % 2 == 0)
		{
			CurrSide = leftBound;
			tempX = CurrSide.transform.position.x - 20;
			
		}
		else
		{
			CurrSide = rightBound;
			tempX = CurrSide.transform.position.x + 20;
		}
		
		tempZ = Random.Range(startBound.transform.position.z,endBound.transform.position.z);
		fireVec = new Vector3(tempX, 0, tempZ);
		
		return fireVec;
	}
	
	Vector3 GetRandomFireVel( GameObject tempAsteroid)
	{
		Vector3 newVelocity;
		float tempX;
		float tempZ;
		
		tempX = Random.Range(asteroidSpeedMin, asteroidSpeedMax);
		tempZ = Random.Range(startBound.transform.position.z, endBound.transform.position.z);
		
		newVelocity = new Vector3(tempX, 0, tempZ);
		
		return newVelocity;
	}
	
	void FindNextFreeAsteroid()
	{
		int i = 0;
		
		while(currentAsteroid == null)
		{
			if(asteroidList[i].rigidbody.velocity == Vector3.zero)
			{
				currentAsteroid = asteroidList[i];
				Debug.Log ("Found obj");
			}
			i++;
		}
	}
	
	public void FireAsteroid()
	{
		FindNextFreeAsteroid();
		
		if(currentAsteroid != null)
		{
			currentAsteroid.SetActive(true);
			Vector3 tempPos = GetRandomFirePos();
			//Vector3 tempVel = GetRandomFireVel();
			currentAsteroid.transform.position = tempPos;
			//currentAsteroid.rigidbody.velocity = tempVel;
			currentAsteroid = null;
		}
	}
}
