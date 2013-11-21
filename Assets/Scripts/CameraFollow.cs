using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float startSize;
	
	public Transform player;
	
	public Transform InsideShip;
	public Transform OutsideShip;
	
	Transform thisTransform;
	Transform outsideCamTransform;
	
	Camera thisCamera;
	public Camera outsideCamera;
	
	Transform target;
	
	float targetSize;
	
	void Start () {
		outsideCamTransform = outsideCamera.transform;
		if(InsideShip == null)
			InsideShip = GameObject.Find("ShipDeck").transform;
		if(OutsideShip == null)
			OutsideShip = GameObject.Find("ShipCraft").transform;
		
		thisCamera = camera;
		thisTransform = transform;
		if(player == null)
			player = GameObject.Find("Player").transform;
		Reset();
	}
	
	void Update () {
		thisTransform.position = Vector3.Lerp(thisTransform.position, target.position + (Vector3.up*20.0f), Time.deltaTime*3.0f);
	//	if(Mathf.Abs(thisCamera.orthographicSize - targetSize) > 0.1f) {
	//		thisCamera.orthographicSize += (targetSize - thisCamera.orthographicSize)*0.1f;
	//	}
		Vector3 offset = thisTransform.position - InsideShip.position;
		outsideCamTransform.position = OutsideShip.position + offset;
		
		thisTransform.eulerAngles = new Vector3(90.0f, -OutsideShip.eulerAngles.y, 0.0f);
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
