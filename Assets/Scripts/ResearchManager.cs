using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
	{
	[SerializeField] Dictionary<GameObject, bool> researched;

	private void Awake()
		{
		DontDestroyOnLoad(this);
		}

	public void addResearch(GameObject buildingtype)
		{
		researched[buildingtype] = true;
		}

	public bool checkPrequisite(GameObject buildingtype)
		{
		return researched[buildingtype];
		}
	}
