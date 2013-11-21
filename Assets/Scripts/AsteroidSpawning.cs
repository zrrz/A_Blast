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
	public int bullshit = 0;
	public float startGap = 10.0f;
	
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
		
		//FireAsteroid();
		//StartCoroutine("FireAsteroid");
	}
	
	// Update is called once per frame
	void Update () {
		if(bullshit < asteroidList.Count)
		{
			FireAsteroid();
			bullshit++;
		}
		
	}
	
	Vector3 GetRandomFirePos()
	{
		Vector3 fireVec;
		GameObject CurrSide;
		float tempX;
		float tempZ;
		
		if(Random.Range(1, 10) % 2 == 0)
		{
			CurrSide = leftBound;
			tempX = CurrSide.transform.position.x - startGap;
			
		}
		else
		{
			CurrSide = rightBound;
			tempX = CurrSide.transform.position.x + startGap;
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
		
		newVelocity = new Vector3(0, 0, tempZ);
		newVelocity.Normalize();
		
		if(currentAsteroid.transform.position.x > rightBound.transform.position.x)
		{
			newVelocity.x = -tempX;
		}
		else if(currentAsteroid.transform.position.x < leftBound.transform.position.x)
		{
			newVelocity.x = tempX;
		}
		
		return newVelocity;
	}
	
	void FindNextFreeAsteroid()
	{
		int i = 0;
		
		while(currentAsteroid == null)
		{
			if(asteroidList[i].activeSelf == false)
			{
				currentAsteroid = asteroidList[i];
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
			currentAsteroid.transform.position = tempPos;
			Vector3 tempVel = GetRandomFireVel(currentAsteroid);
			currentAsteroid.rigidbody.velocity = tempVel;
			currentAsteroid = null;
		}
	}
}
