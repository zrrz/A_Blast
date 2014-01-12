using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject cursor;
	
	
	public float viewModef; 
	public float defaultViewModef;
	public float maxViewDistance;
	public float cameraHeight;
	public float defaultCameraHeight;
	
	
	public Vector3 shakeAmount;
	
	
	public float smoothCamera = 0.3f;
	private Vector3 velocity = Vector3.zero;
	


	public Transform player;
	
	Transform thisTransform;
	
	Camera thisCamera;
	public Camera outsideCamera;
	
	public Transform target;

	public float m_zoomSpeed = 2;
	public float m_heightDistance;
	public float m_distanceHeightMin = 30.0f;
	public float m_distanceHeightMax = 80.0f;
	
	public float startHeight = 60.0f;
	float targetSize;
	
	void Awake () {
		//if(InsideShip == null)
			//InsideShip = GameObject.Find("ShipDeck").transform;
		//if(OutsideShip == null)
			//OutsideShip = GameObject.Find("ShipCraft").transform;
		
		thisCamera = camera;
		thisTransform = transform;
	}

	void Update () {

		if (target){

			SetCursorPos();

			float cursorDistance = Vector2.Distance (Input.mousePosition, new Vector2 (Screen.width / 2, Screen.height / 2)) * cameraHeight;
			Vector3 posMod = (target.transform.position - cursor.transform.position);
			posMod = posMod * viewModef;

			transform.position = Vector3.SmoothDamp(transform.position, target.transform.position - new Vector3 (posMod.x, posMod.y, posMod.z) + shakeAmount + Vector3.up * 10, ref velocity, smoothCamera);
			camera.orthographicSize = Mathf.Lerp (camera.orthographicSize, cameraHeight, 0.1f);

			shakeAmount = Vector3.Lerp (shakeAmount, Vector3.zero, 0.05f);
		
		}

	}
	
	public void ShakeEffect (Vector3 newShakeAmount){
		shakeAmount = newShakeAmount;
	}

	/*
	void Update () {

		if (player != null ) {
			float currAxis = Input.GetAxis ("Mouse ScrollWheel");
			m_heightDistance -= currAxis * m_zoomSpeed;
			m_heightDistance = Mathf.Clamp(m_heightDistance, m_distanceHeightMin, m_distanceHeightMax);
			
			transform.position = Vector3.Lerp (transform.position, target.position + (Vector3.up * m_heightDistance), Time.deltaTime * 3.0f);

			if (Mathf.Abs (thisCamera.orthographicSize - targetSize) > 0.1f) {
				thisCamera.orthographicSize += (targetSize - thisCamera.orthographicSize) * 0.1f;
			}


			//Vector3 offset = new Vector3(Mathf.Cos(-OutsideShip.eulerAngles.y), 0.0f, Mathf.Sin (-OutsideShip.eulerAngles.y));
			//offset.Normalize();


		}
	}
	*/

	void SetCursorPos (){

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit, 500);
		
		cursor.transform.position = new Vector3 (hit.point.x, player.transform.position.y, hit.point.z);

	}

	public void ChangeCam(Transform setTarget, int height, float distance) {
		targetSize = setTarget.GetComponent<CamAnchor>().height;
		target = setTarget;
		cameraHeight = height;
		viewModef = distance;
	}
	
	public void Reset() {
		targetSize = startHeight;
		target = player;
		cameraHeight = defaultCameraHeight;
		viewModef = defaultViewModef;
	}

}
