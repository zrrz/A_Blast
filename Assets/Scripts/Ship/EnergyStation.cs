using UnityEngine;
using System.Collections;

public class EnergyStation : MonoBehaviour {

	public string stationName;
	public int energy;
	public int maxEnergy;
	public int minEnergy;

	void Start () {
		transform.parent.GetComponent<ShipManager>().NewStation(this, stationName, maxEnergy, minEnergy);
	}
	
	public void ChangePower (int power){
		
		energy += power;
		
	}

}
