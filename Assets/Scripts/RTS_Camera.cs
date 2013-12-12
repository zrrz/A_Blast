using UnityEngine;
using System.Collections;

public class RTS_Camera : MonoBehaviour {
	
	public GameObject m_mainBase;
	
	const int PANNING_BUFFER = 20;
	
	Vector3 m_position;
	float m_rotation;
	
	public float m_heightDistance;
	public float m_widthDistance;
	public float m_distanceHeightMin;
	public float m_distanceHeightMax;
	public float m_distanceWidthMin;
	public float m_distanceWidthMax;
	public float m_minAngle;
	public float m_maxAngle;
	
	public float m_panningSpeed;
	public float m_rotateSpeed;
	public float m_zoomSpeed;
	public float m_widthZoomSpeed;
	public Vector3 m_minCameraPos;
	public Vector3 m_maxCameraPos;
	
	private float cameraViewDisplacement = 0.0f;
	public GameObject m_mapMin;
	public GameObject m_mapMax;
	
	public AnimationCurve curve;
	
	// Use this for initialization
	void Start () 
	{			
		if(m_mainBase == null && GameObject.FindGameObjectWithTag("PlayerMainBase"))
		{
			m_mainBase = GameObject.FindGameObjectWithTag("PlayerMainBase");	

			m_position = new Vector3(m_mainBase.transform.position.x , m_mainBase.transform.position.y + m_distanceHeightMax, m_mainBase.transform.position.z - 30);
		}		
		else
		{
			m_position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		}
				
		transform.position = m_position;
		transform.rotation = Quaternion.Euler(new Vector3(m_minAngle, 0, 0));
		
		m_mapMax = GameObject.FindGameObjectWithTag("MapMax");
		m_mapMin = GameObject.FindGameObjectWithTag("MapMin");
		
		m_widthDistance = m_distanceWidthMax;
		m_widthZoomSpeed = m_zoomSpeed * ((m_distanceWidthMax - m_distanceWidthMin)/(m_distanceHeightMax - m_distanceHeightMin));
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		float currAxis = Input.GetAxis("Mouse ScrollWheel");
		
        m_heightDistance -= currAxis * m_zoomSpeed;
		m_heightDistance = Mathf.Clamp(m_heightDistance, m_distanceHeightMin, m_distanceHeightMax);
		
		m_widthDistance += currAxis * m_widthZoomSpeed;
		m_widthDistance = Mathf.Clamp(m_widthDistance, m_distanceWidthMin, m_distanceWidthMax);
		
		//m_rotation -= currAxis * m_rotateSpeed;
		
		//m_rotation = Mathf.Clamp(m_rotation, m_maxAngle, m_minAngle);
		
		//top right corner
		if(Input.mousePosition.x > Screen.width - PANNING_BUFFER && 
			Input.mousePosition.y > Screen.height - PANNING_BUFFER)
		{
			if(transform.position.x + cameraViewDisplacement < m_mapMax.transform.position.x &&
				transform.position.z + cameraViewDisplacement < m_mapMax.transform.position.z)
			{
				m_position.x += m_panningSpeed * Time.deltaTime;
				m_position.z += m_panningSpeed * Time.deltaTime;
			}
		}
		//top left corner
		else if(Input.mousePosition.x < 0.0f + PANNING_BUFFER &&
			Input.mousePosition.y > Screen.width - PANNING_BUFFER)
		{
			if(transform.position.x - cameraViewDisplacement > m_mapMin.transform.position.x &&
				transform.position.z + cameraViewDisplacement < m_mapMax.transform.position.z)
			{
				m_position.x -= m_panningSpeed * Time.deltaTime;
				m_position.z += m_panningSpeed * Time.deltaTime;
			}
		}
		//bottom right corner
		else if(Input.mousePosition.x > Screen.width - PANNING_BUFFER &&
			Input.mousePosition.y < 0.0f + PANNING_BUFFER)
		{
			if(transform.position.x + cameraViewDisplacement < m_mapMax.transform.position.x &&
				transform.position.z - cameraViewDisplacement > m_mapMin.transform.position.z)
			{
				m_position.x += m_panningSpeed * Time.deltaTime;
				m_position.z -= m_panningSpeed * Time.deltaTime;
			}
		}
		//bottom left corner
		else if(Input.mousePosition.x < 0.0f + PANNING_BUFFER &&
			Input.mousePosition.y < 0.0f + PANNING_BUFFER)
		{
			if(transform.position.x - cameraViewDisplacement > m_mapMin.transform.position.x &&
				transform.position.z - cameraViewDisplacement >  m_mapMin.transform.position.z)
			{
				m_position.x -= m_panningSpeed * Time.deltaTime;
				m_position.z -= m_panningSpeed * Time.deltaTime;
			}
		}
		//right
		else if(Input.mousePosition.x > Screen.width - PANNING_BUFFER){
			if(m_position.x < m_mapMax.transform.position.x)
				m_position.x += m_panningSpeed * Time.deltaTime;
		}
		//left
		else if(Input.mousePosition.x < 0.0f + PANNING_BUFFER){
			if(m_position.x > m_mapMin.transform.position.x)
				m_position.x -= m_panningSpeed * Time.deltaTime;
		}
		//up
		else if(Input.mousePosition.y > Screen.height - PANNING_BUFFER){
			if(m_position.z < m_mapMax.transform.position.z)
				m_position.z += m_panningSpeed * Time.deltaTime;
		}
		//down
		else if(Input.mousePosition.y < 0.0f + PANNING_BUFFER){
			if(m_position.z + m_widthDistance * (m_heightDistance/m_distanceHeightMax) > m_mapMin.transform.position.z)
				m_position.z -= m_panningSpeed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.Space) && m_mainBase != null)
			m_position = new Vector3(m_mainBase.transform.position.x, 
									 m_position.y,
									 m_mainBase.transform.position.z - 30);
		
		
		/*
		if(m_heightDistance < m_distanceHeightMin + 10)
		{
			float change = m_heightDistance - m_distanceHeightMin;
			m_rotation = m_maxAngle + change * 1.2f;
			m_rotation = Mathf.Clamp(m_rotation, m_maxAngle, m_minAngle);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(m_rotation,0,0)), Time.deltaTime);
		}
		else
		{
			m_rotation = m_minAngle;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(m_rotation,0,0)), Time.deltaTime);
		}
		*/
		
		//if(m_position.z - m_widthDistance < m_mapMin.transform.position.z)
			//m_position.z = m_mapMin.transform.position.z + m_widthDistance - 10;
		
		transform.position = Vector3.Lerp(transform.position,new Vector3(m_position.x, m_position.y + m_heightDistance, m_position.z + m_widthDistance), Time.deltaTime * 2);
		//m_camera.transform.position = new Vector3(m_position.x, transform.position.y + m_distance, m_position.z);
		
	}
}
