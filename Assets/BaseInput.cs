using UnityEngine;
using System.Collections;

public class BaseInput : MonoBehaviour {
	
	protected Vector3 _dir;
	protected bool _button1;
	protected bool _shift;

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	public Vector3 dir {
		get {
			return _dir;
		}
	}
	
	public bool button1 {
		get {
			return _button1;
		}
	}
	
	public bool shift {
		get {
			return _shift;
		}
	}
}
