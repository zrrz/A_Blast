using UnityEngine;
using System.Collections;

public class BaseInput : MonoBehaviour {
	
	protected Vector3 _dir;
	protected Vector3 _targetDir;
	protected bool _fire;
	protected bool _shift;

	public bool getUserInput = false;

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	public Vector3 dir {
		get {
			return _dir;
		}
	}

	public Vector3 targetDir {
		get {
			return _targetDir;
		}
	}
	
	public bool fire {
		get {
			return _fire;
		}
	}
	
	public bool shift {
		get {
			return _shift;
		}
	}
}
