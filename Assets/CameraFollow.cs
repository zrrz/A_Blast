using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float startSize;
	
	public Transform player;
	
	Transform thisTransform;
	
	Camera thisCamera;
	
	Transform target;
	
	float targetSize;
	
	void Start () {
		thisCamera = camera;
		thisTransform = transform;
		if(player == null)
			player = GameObject.Find("Player").transform;
		Reset();
	}
	
	void Update () {
		thisTransform.position = Vector3.Lerp(thisTransform.position, target.position + (Vector3.up*10.0f), Time.deltaTime*3.0f);
		if(Mathf.Abs(thisCamera.orthographicSize - targetSize) > 0.1f) {
			thisCamera.orthographicSize += (targetSize - thisCamera.orthographicSize)*0.1f;
		}
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
