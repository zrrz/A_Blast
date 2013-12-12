using UnityEngine;
using System.Collections;

public class EnemyShipMove : MonoBehaviour {

	public float maxHealth = 5;
	
	float health;
	
	Transform thisTransform;
	
	public float spinSpeed = 25.0f;

	BaseInput input;
	
	public float moveSpeed = 5.0f;
	
	CharacterController characterController;

	float shootTimer = 0.0f;

	public float shootCD = 2.0f;

	public GameObject bullet;

	public Texture healthBar;
	Camera cam;
	Vector3 offset = new Vector3(0.0f, 1.0f, 3.0f);
	
	void Start () {
		health = maxHealth;
		thisTransform = transform;
		input = GetComponent<BaseInput>();
		characterController = GetComponent<CharacterController>();
		cam = GameObject.Find("Camera").GetComponent<Camera>();
	}
	
	void Update () {
		thisTransform.Rotate(Vector3.up, spinSpeed*Time.deltaTime);
		characterController.Move(input.dir * moveSpeed * Time.deltaTime);
		shootTimer += Time.deltaTime;
		if (input.fire) {
			if(shootTimer > shootCD) {
				shootTimer = 0.0f;
				Instantiate(bullet, transform.position, Quaternion.LookRotation(input.targetDir));
			}
		}
	}

	void TakeDamage(float damage) {
		health -= damage;
		if(health <= 0) {
			Destroy(gameObject);
		}
	}

	void OnGUI () {
		Vector3 pos = cam.WorldToScreenPoint(transform.position + offset);

		float width = Screen.width * 0.07f;
		float height = Screen.height * 0.01f;

		float hp = health / maxHealth;
		GUI.DrawTexture(new Rect(pos.x - width*0.4f, Screen.height - pos.y, width*hp, height), healthBar);
	}
}
