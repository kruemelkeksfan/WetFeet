﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
	{
	[SerializeField] ResourceManager resourcemanager;
	[SerializeField] WaterManager watermanager;
	[SerializeField] Button launch;
	[SerializeField] WarningManager warningmanager;
	[SerializeField] int[] smallhouse;
	[SerializeField] int[] apartmentblock;
	[SerializeField] int[] skyscraper;
	[SerializeField] int[] mason;
	[SerializeField] int[] concretefactory;
	[SerializeField] int[] glassworks;
	[SerializeField] int[] sawmill;
	[SerializeField] int[] mine;
	[SerializeField] int[] pumpjack;
	[SerializeField] int[] manufacture;
	[SerializeField] int[] factory;
	[SerializeField] int[] hightechfactory;
	[SerializeField] int[] farm;
	[SerializeField] int[] hydroponicfarm;
	[SerializeField] int[] spaceport;

	private List<GameObject> structures;
	private Dictionary<BuildableStructure.Buildingtype, int> structurecounts;
	private Dictionary<BuildableStructure.Buildingtype, int[]> buildcosts;
	private GameObject currentbuilding;

	private void Start()
		{
		structures = new List<GameObject>();

		structurecounts = new Dictionary<BuildableStructure.Buildingtype, int>();
		foreach(BuildableStructure.Buildingtype type in System.Enum.GetValues(typeof(BuildableStructure.Buildingtype)))
			{
			structurecounts.Add(type, 0);
			}

		buildcosts = new Dictionary<BuildableStructure.Buildingtype, int[]>();
		buildcosts.Add(BuildableStructure.Buildingtype.smallhouse, smallhouse);
		buildcosts.Add(BuildableStructure.Buildingtype.apartmentblock, apartmentblock);
		buildcosts.Add(BuildableStructure.Buildingtype.skyscraper, skyscraper);
		buildcosts.Add(BuildableStructure.Buildingtype.mason, mason);
		buildcosts.Add(BuildableStructure.Buildingtype.concretefactory, concretefactory);
		buildcosts.Add(BuildableStructure.Buildingtype.glassworks, glassworks);
		buildcosts.Add(BuildableStructure.Buildingtype.sawmill, sawmill);
		buildcosts.Add(BuildableStructure.Buildingtype.mine, mine);
		buildcosts.Add(BuildableStructure.Buildingtype.pumpjack, pumpjack);
		buildcosts.Add(BuildableStructure.Buildingtype.manufacture, manufacture);
		buildcosts.Add(BuildableStructure.Buildingtype.factory, factory);
		buildcosts.Add(BuildableStructure.Buildingtype.hightechfactory, hightechfactory);
		buildcosts.Add(BuildableStructure.Buildingtype.farm, farm);
		buildcosts.Add(BuildableStructure.Buildingtype.hydroponicfarm, hydroponicfarm);
		buildcosts.Add(BuildableStructure.Buildingtype.spaceport, spaceport);
		}

	void Update()
		{
		if(currentbuilding != null) // while placing building
			{
			BuildableStructure currentBuildableStructure = currentbuilding.GetComponent<BuildableStructure>();
			if(currentBuildableStructure.hasPlaced()) // if building placed
				{
				if(resourcemanager.subtractResources(buildcosts[currentBuildableStructure.getBuildingType()]))
					{
					addStructure(currentbuilding);
					resourcemanager.unsetProjectCosts();

					if(currentBuildableStructure.getBuildingType() == BuildableStructure.Buildingtype.spaceport)
						{
						launch.gameObject.SetActive(true);
						}

					currentbuilding = null;
					}
				else
					{
					cancelBuilding();
					warningmanager.setWarning("Not enough resources");
					}
				}
			if(currentbuilding != null && Input.GetKeyDown(KeyCode.R))
				{
				Vector3 currentRotation = currentbuilding.transform.rotation.eulerAngles;
				currentbuilding.transform.rotation = Quaternion.Euler(currentRotation + new Vector3(0, 90, 0));
				}
			if(Input.GetMouseButtonDown(1)) // if building cancelled
				{
				cancelBuilding();
				}
			}
		}

	public void InstantiateBuilding(BuildableStructure building)
		{
		cancelBuilding();       // TODO: bug: sometimes building gets placed before InstantiateBuilding() and therefore cancelBuilding is called
		currentbuilding = Instantiate(building.gameObject, Vector3.zero, Quaternion.identity);
		currentbuilding.GetComponent<BuildableStructure>().setWarningManager(warningmanager);
		resourcemanager.setProjectCosts(buildcosts[currentbuilding.GetComponent<BuildableStructure>().getBuildingType()]);
		}

	public void cancelBuilding()
		{
		if(currentbuilding != null)
			{
			resourcemanager.unsetProjectCosts();
			Object.Destroy(currentbuilding);
			currentbuilding = null;
			}
		}

	private void addStructure(GameObject structure)
		{
		++structurecounts[structure.GetComponent<BuildableStructure>().getBuildingType()];
		structures.Add(structure);
		}

	private void destroyStructure(GameObject structure)
		{
		--structurecounts[structure.GetComponent<BuildableStructure>().getBuildingType()];
		structures.Remove(structure);
		Object.Destroy(structure);
		}

	public int getBuildingCount(BuildableStructure.Buildingtype type)
		{
		return structurecounts[type];
		}

	public void checkWaterline()
		{
		float sealevel = watermanager.getSealevel();
		List<GameObject> flooded = new List<GameObject>();
		foreach(GameObject structure in structures)
			{
			if(structure.transform.position.y <= sealevel - 1)
				{
				flooded.Add(structure);
				}
			}

		for(int I = 0; I < flooded.Count; ++I)
			{
			destroyStructure(flooded[I]);
			}
		}
	}
