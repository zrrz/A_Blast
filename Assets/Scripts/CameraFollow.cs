using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public Transform player;
	
	public Transform InsideShip;
	public Transform OutsideShip;
	
	Transform thisTransform;
	Transform outsideCamTransform;
	
	Camera thisCamera;
	public Camera outsideCamera;
	
	Transform target;

	public float m_zoomSpeed = 2;
	public float m_heightDistance;
	public float m_distanceHeightMin = 30.0f;
	public float m_distanceHeightMax = 80.0f;
	
	public float startHeight = 60.0f;
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
			player = GameObject.Find("Player(Clone)").transform;
		Reset();
	}
	
	void Update () {
	
		if (player) {

			float currAxis = Input.GetAxis ("Mouse ScrollWheel");
			m_heightDistance -= currAxis * m_zoomSpeed;
			m_heightDistance = Mathf.Clamp(m_heightDistance, m_distanceHeightMin, m_distanceHeightMax);
			
			thisTransform.position = Vector3.Lerp (thisTransform.position, target.position + (Vector3.up * m_heightDistance), Time.deltaTime * 3.0f);

			if (Mathf.Abs (thisCamera.orthographicSize - targetSize) > 0.1f) {
					thisCamera.orthographicSize += (targetSize - thisCamera.orthographicSize) * 0.1f;
			}
			Vector3 offset = thisTransform.position - InsideShip.position;
			outsideCamTransform.position = OutsideShip.position + offset;
			outsideCamera.orthographicSize = thisCamera.orthographicSize;

			thisTransform.eulerAngles = new Vector3 (90.0f, -OutsideShip.eulerAngles.y, 0.0f);
		}
	}
	
	public void ChangeCam(Transform setTarget) {
		targetSize =  setTarget.GetComponent<CamAnchor>().height;
		target = setTarget;
	}
	
	public void Reset() {
		targetSize = startHeight;
		target = player;
	}
}
