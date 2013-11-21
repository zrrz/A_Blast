using UnityEngine;
using System.Collections;

public class PointAt : MonoBehaviour {
	
	public Transform target;
	
	Transform thisTransform;
	
	void Start () {
		thisTransform = transform;
	}
	
	void Update () {
		thisTransform.LookAt(target.position);
	}
}
