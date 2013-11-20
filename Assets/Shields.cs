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
	public int activeRightBars = 2;
	public int activeLeftBars = 2;
	public int activeTopBars = 2;
	public int activeBottomBars = 2;
	
	// Use this for initialization
	void Start () {
		tempBarList = GameObject.FindGameObjectsWithTag("ShieldBar");
		m_position = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetKey(KeyCode.Space))
		{
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				if(activeRightBars < 3 && freeBars > 0)
				{
					RightBarList[activeRightBars].SetActive(true);
					freeBars--;
					activeRightBars++;
				}
			}
			else if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if(activeLeftBars < 3 && freeBars > 0)
				{
					LeftBarList[activeLeftBars].SetActive(true);
					freeBars--;
					activeLeftBars++;
				}
			}
			else if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				if(activeTopBars < 3 && freeBars > 0)
				{
					TopBarList[activeTopBars].SetActive(true);
					freeBars--;
					activeTopBars++;
				}
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				if(activeBottomBars < 3 && freeBars > 0)
				{
					BottomBarList[activeBottomBars].SetActive(true);
					freeBars--;
					activeBottomBars++;
				}
			}
		}
		
		if(Input.GetKey(KeyCode.Space))
		{
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				if(RightBarList.Count > 0)
				{
					RightBarList[activeRightBars-1].SetActive(false);
					freeBars++;
					activeRightBars--;
				}
			}
			else if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if(LeftBarList.Count > 0)
				{
					LeftBarList[activeLeftBars-1].SetActive(false);
					freeBars++;
					activeLeftBars--;
				}
			}
			else if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				if(TopBarList.Count > 0)
				{
					TopBarList[activeTopBars-1].SetActive(false);
					freeBars++;
					activeTopBars--;
				}
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				if(BottomBarList.Count > 0)
				{
					BottomBarList[activeBottomBars-1].SetActive(false);
					freeBars++;
					activeBottomBars--;
				}
			}
		}
	}
	
	void ReorderBars(List<GameObject> sentList, string side)
	{
		if(side == "right")
		{
			foreach(GameObject bar in sentList)
			{
				
			}
		}
		if(side == "left")
		{
			foreach(GameObject bar in sentList)
			{
				
			}
		}
		if(side == "top")
		{
			foreach(GameObject bar in sentList)
			{
				
			}
		}
		if(side == "bottom")
		{
			foreach(GameObject bar in sentList)
			{
				
			}
		}
		/*
		int i = 0;
		if(side == "right")
		{
			foreach(GameObject bar in sentList)
			{
				Quaternion tempRot = bar.transform.rotation;
				Vector3 tempPos = bar.transform.position;
				tempRot.y = 0;
				tempPos = new Vector3(m_position.x + 20 + (i*barDisplacement), m_position.y, m_position.z);
				bar.transform.rotation = tempRot;
				bar.transform.position = tempPos;
				i++;
			}
		}
		if(side == "left")
		{
			foreach(GameObject bar in sentList)
			{
				Quaternion tempRot = bar.transform.rotation;
				Vector3 tempPos = bar.transform.position;
				tempRot.y = 0;
				tempPos = new Vector3(m_position.x - 20 - (i*barDisplacement), m_position.y, m_position.z);
				bar.transform.rotation = tempRot;
				bar.transform.position = tempPos;
				i++;
			}
		}
		if(side == "top")
		{
			foreach(GameObject bar in sentList)
			{
				Quaternion tempRot = bar.transform.rotation;
				Vector3 tempPos = bar.transform.position;
				tempRot.y = 90;
				tempPos = new Vector3(m_position.x, m_position.y, m_position.z + 20 + (i*barDisplacement));
				bar.transform.rotation = tempRot;
				bar.transform.position = tempPos;
				i++;
			}
		}
		if(side == "bottom")
		{
			foreach(GameObject bar in sentList)
			{
				Quaternion tempRot = bar.transform.rotation;
				Vector3 tempPos = bar.transform.position;
				tempRot.y = 90;
				tempPos = new Vector3(m_position.x, m_position.y, m_position.z - 20 - (i*barDisplacement));
				bar.transform.rotation = tempRot;
				bar.transform.position = tempPos;
				i++;
			}
		}
		*/
		
	}
}
