using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float startSize;
	
	public Transform player;
	public Transform ship;
	
	Transform thisTransform;
	
	Camera thisCamera;
	
	Vector3 playeroffset;
	Vector3	ShipOffset;
	
	Transform target;
	
	float targetSize;
	
	void Start () {
		thisCamera = camera;
		thisTransform = transform;
		if(player == null)
			player = GameObject.Find("Player").transform;
		if(ship == null)
			ship = GameObject.Find("ship3Model").transform;
		Reset();
		
		playeroffset.Set(0,10,-3);
		ShipOffset.Set (0,50,-18);
	}
	
	void Update () {
		if(player.GetComponent<PlayerMove>().UsingDevice)
		{
			thisTransform.position = Vector3.Lerp(thisTransform.position, ship.position + ShipOffset, Time.deltaTime*4.0f);
		}
		else
		{
			thisTransform.position = Vector3.Lerp(thisTransform.position, player.position + playeroffset, Time.deltaTime*4.0f);
		}
		
		/*
		if(Mathf.Abs(thisCamera.orthographicSize - targetSize) > 0.1f) {
			thisCamera.orthographicSize += (targetSize - thisCamera.orthographicSize)*0.1f;
			
		}
		*/
	}
	
	public void ChangeCam(Transform setTarget, float setSize) {
		targetSize = setSize;
		target = setTarget;
	}
	
	public void Reset() {
		targetSize = startSize;
		target = player;
	}
}
