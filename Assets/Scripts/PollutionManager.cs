using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutionManager : MonoBehaviour
	{
	[SerializeField] float smallhouse;
	[SerializeField] float apartmentblock;
	[SerializeField] float skyscraper;
	[SerializeField] float mason;
	[SerializeField] float concretefactory;
	[SerializeField] float glassworks;
	[SerializeField] float sawmill;
	[SerializeField] float mine;
	[SerializeField] float pumpjack;
	[SerializeField] float manufacture;
	[SerializeField] float factory;
	[SerializeField] float hightechfactory;
	[SerializeField] float farm;
	[SerializeField] float hydroponicfarm;
	[SerializeField] float spaceport;
	[SerializeField] BuildingManager buildingmanager;
	[SerializeField] ResourceManager resourcemanager;
	[SerializeField] GameObject water;
	[SerializeField] int updatefrequency = 500;

	private float pollution = 0.2f;
	private int updatecounter;
	private Dictionary<BuildableStructure.Buildingtype, float> pollutionvalues;

	private void Start()
		{
		pollutionvalues = new Dictionary<BuildableStructure.Buildingtype, float>();
		pollutionvalues.Add(BuildableStructure.Buildingtype.smallhouse, smallhouse);
		pollutionvalues.Add(BuildableStructure.Buildingtype.apartmentblock, apartmentblock);
		pollutionvalues.Add(BuildableStructure.Buildingtype.skyscraper, skyscraper);
		pollutionvalues.Add(BuildableStructure.Buildingtype.mason, mason);
		pollutionvalues.Add(BuildableStructure.Buildingtype.concretefactory, concretefactory);
		pollutionvalues.Add(BuildableStructure.Buildingtype.glassworks, glassworks);
		pollutionvalues.Add(BuildableStructure.Buildingtype.sawmill, sawmill);
		pollutionvalues.Add(BuildableStructure.Buildingtype.mine, mine);
		pollutionvalues.Add(BuildableStructure.Buildingtype.pumpjack, pumpjack);
		pollutionvalues.Add(BuildableStructure.Buildingtype.manufacture, manufacture);
		pollutionvalues.Add(BuildableStructure.Buildingtype.factory, factory);
		pollutionvalues.Add(BuildableStructure.Buildingtype.hightechfactory, hightechfactory);
		pollutionvalues.Add(BuildableStructure.Buildingtype.farm, farm);
		pollutionvalues.Add(BuildableStructure.Buildingtype.hydroponicfarm, hydroponicfarm);
		pollutionvalues.Add(BuildableStructure.Buildingtype.spaceport, spaceport);
		}

	void FixedUpdate()
		{
		if(++updatecounter >= updatefrequency)
			{
			Dictionary<BuildableStructure.Buildingtype, int> structurecounts = resourcemanager.getStructureCounts(); // TODO: kill it with fire
			foreach(BuildableStructure.Buildingtype structure in structurecounts.Keys)
				{
				pollution += (pollutionvalues[structure]) * structurecounts[structure];
				pollution += (pollutionvalues[structure]) * structurecounts[structure];
				pollution += (pollutionvalues[structure]) * structurecounts[structure];
				pollution += (pollutionvalues[structure]) * structurecounts[structure];
				pollution += (pollutionvalues[structure]) * structurecounts[structure];
				}
			updatecounter = 0;
			}

		water.transform.position = new Vector3(water.transform.position.x, pollution, water.transform.position.z);
		buildingmanager.checkWaterline(pollution - 1);
		}
	}
