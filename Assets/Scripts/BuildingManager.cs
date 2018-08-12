using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void InsatciateBuilding(BuildableStructure building)
    {
        GameObject currentBuilding = Instantiate(building.gameObject, transform.position, Quaternion.identity);
        BuildableStructure buildableStruckture = currentBuilding.GetComponent<BuildableStructure>();
    }
}
