using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shields : MonoBehaviour {
	
	public List<GameObject> ShieldBarList;
	public List<GameObject> LeftBarList;
	public List<GameObject> RightBarList;
	public List<GameObject> TopBarList;
	public List<GameObject> BottomBarList;
	public Vector3 m_position;
	public GameObject[] tempBarList;
	
	public int freeBars = 0;
	
	public int tempIndex;
	
	public int totalBars = 12;
	
	// Use this for initialization
	void Start () {
		tempBarList = GameObject.FindGameObjectsWithTag("ShieldBar");
		m_position = transform.position;
		
		foreach(GameObject bar in RightBarList)
		{
			bar.GetComponent<ShieldBar>().currList = RightBarList;
		}
		
		foreach(GameObject bar in LeftBarList)
		{
			bar.GetComponent<ShieldBar>().currList = LeftBarList;
		}
		
		foreach(GameObject bar in TopBarList)
		{
			bar.GetComponent<ShieldBar>().currList = TopBarList;
		}
		
		foreach(GameObject bar in BottomBarList)
		{
			bar.GetComponent<ShieldBar>().currList = BottomBarList;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetKey(KeyCode.Space))
		{
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				AddBar(RightBarList);
					//RightBarList[rightBarIndex].SetActive(true);
					//freeBars--;
					//rightBarIndex++;
					//activeRightBars++;
			}
			else if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				AddBar(LeftBarList);
					//LeftBarList[leftBarIndex].SetActive(true);
					//freeBars--;
					//leftBarIndex++;
					//activeLeftBars++;
			}
			else if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				AddBar(TopBarList);
					//TopBarList[topBarIndex].SetActive(true);
					//freeBars--;
				//	topBarIndex++;
				//	activeTopBars++;
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
					AddBar(BottomBarList);
					//BottomBarList[bottomBarIndex].SetActive(true);
					//freeBars--;
					//bottomBarIndex++;
					//activeBottomBars++;
			}
		}
		
		if(Input.GetKey(KeyCode.Space))
		{
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				RemoveToFreeBar(RightBarList);
					//RightBarList[rightBarIndex].SetActive(false);
			}
			else if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				RemoveToFreeBar(LeftBarList);
					//LeftBarList[leftBarIndex].SetActive(false);
			}
			else if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				RemoveToFreeBar(TopBarList);
					//TopBarList[topBarIndex].SetActive(false);
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
					//RemoveToFreeBar(BottomBarList, bottomBarIndex);
				RemoveToFreeBar(BottomBarList);
					//BottomBarList[bottomBarIndex].SetActive(false);
			}
		}
	}
	
	public int FindActiveIndex(List<GameObject> tlist)
	{
		int newIndex =0;
		foreach(GameObject bar in tlist)
		{
			if(bar.activeSelf == true)
				newIndex++;
		}
		
		return newIndex -1;
	}
	
	void AddBar( List<GameObject> templist)
	{
		int index = FindActiveIndex(templist);
		index++;
		if(index < 3 && freeBars > 0)
		{
			templist[index].SetActive(true);
			freeBars--;
			totalBars++;
		}
	}
	
	public void RemoveBar( List<GameObject> templist)
	{
		int index = FindActiveIndex(templist);
		if(index > 0)
		{
			totalBars--;
			templist[index].SetActive(false);
			
		}
	}
	
	public void RemoveToFreeBar( List<GameObject> templist)
	{
		int index = FindActiveIndex(templist);
		if(index > 0)
		{
			freeBars++;
			totalBars--;
			templist[index].SetActive(false);
		}
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(40,40,50,200), "Total Shields left = " + totalBars);
		GUI.Label(new Rect(40,100,50,200), "Unused Bars = " + freeBars);
	}
}
