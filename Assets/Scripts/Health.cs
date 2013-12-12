using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int m_maxHealth = 100;
	public int m_health = 100;
	
	public Texture m_healthBar;
	public Texture m_healthBarHolder;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TakeDamage(float damage) {
		m_health -= (int)damage;
	}
	
	void OnGUI () {
		float x = Screen.width * 0.5f;
		float y = Screen.height * 0.03f;
		float w = Screen.width * 0.4f;
		float h = Screen.height * 0.03f;
		float hp = Mathf.Clamp((float)m_health/(float)m_maxHealth, 0.0f, 1.0f);

		GUI.DrawTexture(new Rect(x - w/2.0f, y, w * hp, h), m_healthBar);
		GUI.DrawTexture(new Rect(x - w/2.0f, y, w, h), m_healthBarHolder);
	}
}
