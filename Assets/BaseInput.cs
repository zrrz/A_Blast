using UnityEngine;
using System.Collections;

public class BaseInput : MonoBehaviour {
	
	protected Vector3 _dir;

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	public Vector3 dir {
		get {
			return _dir;
		}
	}
}
