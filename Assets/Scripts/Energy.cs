using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public float m_maxEnergy = 100;
	public float m_energy = 100;
	
	public Texture m_energyBar;
	public Texture m_energyBarHolder;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(m_energy < m_maxEnergy)
			m_energy += 0.7f;
	}
	
	public void ReduceEnergy(float energy) {
		m_energy -= energy;
	}

	void OnGUI () {
		float x = Screen.width * 0.97f;
		float y = Screen.height * 0.5f;
		float w = Screen.width * 0.03f;
		float h = Screen.height * 0.4f;
		float nrg = Mathf.Clamp(m_energy/m_maxEnergy, 0.0f, 1.0f);

		GUI.DrawTexture(new Rect(x, y, w, h* nrg), m_energyBar);
		GUI.DrawTexture(new Rect(x, y, w, h), m_energyBarHolder);
		GUI.Label(new Rect(x + 5, y + 40, 10.0f, 100.0f), "E N E R G Y");
	}
}
