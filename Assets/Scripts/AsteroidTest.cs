using UnityEngine;
using System.Collections;

public class AsteroidTest : MonoBehaviour {

	public Material[] materials;
    public Material baseMaterial;
	public GameObject sphere;
    public float duration = 2.0F;
	public float timer = 0.0f;
	public int i = 0;
	
    void Start() {
       sphere.renderer.material = materials[0];
    }
    void Update() {
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
		sphere.renderer.material.Lerp(materials[i], baseMaterial, lerp);
		//else
		//	sphere.renderer.material.Lerp(baseMaterial, materials[i], lerp);
    }
}
