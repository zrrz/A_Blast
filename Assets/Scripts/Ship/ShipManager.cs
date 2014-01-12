using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipManager : MonoBehaviour {

	public int energy;
	public int maxEnergy;

	public List<Station> stations = new List<Station>();

	void Start () {

	}
	
	void Update () {
	
	}

	void OnGUI (){

		GUI.Box (new Rect (20, 20, 75, 75), "Energy " + energy + "/" + maxEnergy);
		
		for (int s=0; s<stations.Count; s++){

			GUI.Box (new Rect (20, 100+75*s, 75, 75), stations[s].name + "\n" + stations[s].energy);
			if (GUI.Button (new Rect(95, 100+75*s, 37, 37), "+") && energy > 0)
				ChangePower (s, 1);
			if (GUI.Button (new Rect(95, 100+75*s+37, 37, 37), "-") && stations[s].energy > 0)
				ChangePower (s, -1);

		}

	}

	[RPC]
	public void ChangePower (int station, int power){

		stations[station].energy += power;
		stations[station].stationScript.ChangePower (power);
		energy -= power;

	}


	public void NewStation (EnergyStation newStationScript, string newName, int min, int max){

		Station newStation = new Station();
		newStation.stationScript = newStationScript;
		newStation.name = newName;
		newStation.maxEnergy = max;
		newStation.minEnergy = min;
		stations.Add (newStation);

	}

	public class Station {

		public EnergyStation stationScript; 
		public string name;
		public int energy;
		public int maxEnergy;
		public int minEnergy;

	}

}
