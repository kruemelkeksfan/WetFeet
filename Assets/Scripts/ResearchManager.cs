using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
	{
	// variable: List of accomplished researches

	private void Awake()
		{
		DontDestroyOnLoad(this);
		// initialize list
		}

	// adds the given research to the list of accomplished researches
	public void addResearch(string researchname)
		{

		}

	// returns, whether the given building is already researched
	public bool checkPrequisite(GameObject buildingtype)
		{
		return true;
		}
	}
