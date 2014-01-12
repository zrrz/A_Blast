using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {

	public int posetive;
	public float maxLeftAngle = 30;
	public float maxRightAngle = 30;
	public float turnSpeed = 35.0f;
	public float maxSpeed = 10;

	public float force;
	public Transform forcePos;
	public Rigidbody rigid;

	void Start () {
	
	}
	
	void Update () {

		float rotate = Input.GetAxis ("Vertical") * posetive;
		//transform.localEulerAngles = new Vector3 (0, transform.localEulerAngles.y + , 0);
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate * turnSpeed, turnSpeed * Time.deltaTime);
		transform.localEulerAngles = new Vector3 (0, Mathf.Clamp(transform.localEulerAngles.y + rotate, maxLeftAngle, maxRightAngle), 0);
		rigid.AddForceAtPosition ((forcePos.position - transform.position) * force, forcePos.position);




	}
}
