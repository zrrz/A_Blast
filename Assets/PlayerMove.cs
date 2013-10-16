using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	
	BaseInput input;
	
	Transform thisTransform;
	
	public bool used = true;
	
	public float moveSpeed = 5.0f;
			
	void Start () {
		thisTransform = transform;
		input = GetComponent<BaseInput>();
	}
	
	void Update () {
		if(used) {
			thisTransform.position += input.dir * moveSpeed * Time.deltaTime;
		}
	}
}
