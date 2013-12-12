using UnityEngine;
using System.Collections;

public class AsteroidTest : MonoBehaviour {

	public Material[] materials;
    public Material baseMaterial;
	public GameObject sphere;
    public float duration = 2.0F;
	public float timer = 0.0f;
	public int i = 0;
	public float inflation = 0.0f;
	public float maxInflation = 6.0f;
	public float minInflation;
	
    void Start() {
		sphere.renderer.material = materials[0];
		minInflation = transform.localScale.x;
		inflation = minInflation;
    }
    void Update() {
		
		if(inflation > minInflation)
			reduceScale();
		
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
		timer+= Time.deltaTime;	
		if( timer >= duration)
		{
			i++;
			timer = 0;
			if(i > materials.Length)
				i = 0;
		}
		//if(i % 2 == 0)
		//sphere.renderer.material.Lerp(materials[i], baseMaterial, lerp);
		//else
		//	sphere.renderer.material.Lerp(baseMaterial, materials[i], lerp);
    }
	
	public void IncreaseScale( float increaseAmount)
	{
		inflation+=increaseAmount;
		//Mathf.Clamp(inflation, minInflation, maxInflation);
		transform.localScale = new Vector3(inflation,0,inflation);
	}
	
	void reduceScale()
	{
		inflation -= 0.05f;
		//Mathf.Clamp(inflation, minInflation, maxInflation);
		transform.localScale = new Vector3(inflation,0,inflation);
	}
}
