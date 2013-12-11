using UnityEngine;
using System.Collections;

public class AsteroidGrabber : MonoBehaviour {
	
	public Collider[] asteroidList;
	//public GameObject selectionSphere;
	public Material[] materialList;
	public Material mat1;
	public Material mat2;
	public bool IsGrabbing = false;
	private float timer = 0.0f;
	private float timercap = 1.0f;
	private int materialIndex = 1;
	private Material tempmat;
	public float t;
	
	
	// Use this for initialization
	void Start () {
		//tempmat = renderer.material;
		//selectionSphere.renderer.material = tempmat;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(materialIndex < materialList.Length)
		{
			timer += Time.deltaTime;
			Debug.Log(timer);
			if(timer > timercap)
			{

				timer = 0.0f;
			}
			materialIndex++;
		}
		
		//selectionSphere.renderer.material.Lerp(tempmat,materialList[6], t);
		//selectionSphere.renderer.material = tempmat;
		
		/*
		if(Input.GetKeyDown(KeyCode.H))
		{
			if(IsGrabbing)
			{
				
			}
			else
			{
				Vector3 center = selectionSphere.transform.position;
				float radius  = selectionSphere.GetComponent<SphereCollider>().radius;
				asteroidList = Physics.OverlapSphere(center, radius);	
			}
			
			IsGrabbing = !IsGrabbing;
		}
		
		if(IsGrabbing)
		{
			
		}
		*/
	}
}
