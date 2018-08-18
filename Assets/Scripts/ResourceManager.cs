using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
	{
	[SerializeField] BuildingManager buildingmanager;
	[SerializeField] Text resourcedisplay;
	[SerializeField] int updatefrequency = 50;
	[SerializeField] int[] startresources;
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

	public enum Resourcetype
		{
		workforce,
		buildingmaterials,
		resources,
		goods,
		food
		}

	private Dictionary<Resourcetype, int> resourceindices;
	private Dictionary<Resourcetype, string> resourcenames;
	private Dictionary<Resourcetype, int> resources;
	private Dictionary<BuildableStructure.Buildingtype, int[]> productionvalues;
	private int updatecounter = 0;
	private int[] projectcosts;

	private void Start()
		{
		resourceindices = new Dictionary<Resourcetype, int>(5);
		resourceindices.Add(Resourcetype.workforce, 0);
		resourceindices.Add(Resourcetype.buildingmaterials, 1);
		resourceindices.Add(Resourcetype.resources, 2);
		resourceindices.Add(Resourcetype.goods, 3);
		resourceindices.Add(Resourcetype.food, 4);

		resourcenames = new Dictionary<Resourcetype, string>(5);
		resourcenames.Add(Resourcetype.workforce, "Workforce");
		resourcenames.Add(Resourcetype.buildingmaterials, "Building Materials");
		resourcenames.Add(Resourcetype.resources, "Resources");
		resourcenames.Add(Resourcetype.goods, "Goods");
		resourcenames.Add(Resourcetype.food, "Food");

		resources = new Dictionary<Resourcetype, int>(5);
		foreach(Resourcetype type in Enum.GetValues(typeof(Resourcetype)))
			{
			resources.Add(type, startresources[resourceindices[type]]);
			}

		unsetProjectCosts();

		productionvalues = new Dictionary<BuildableStructure.Buildingtype, int[]>();
		productionvalues.Add(BuildableStructure.Buildingtype.smallhouse, smallhouse);
		productionvalues.Add(BuildableStructure.Buildingtype.apartmentblock, apartmentblock);
		productionvalues.Add(BuildableStructure.Buildingtype.skyscraper, skyscraper);
		productionvalues.Add(BuildableStructure.Buildingtype.mason, mason);
		productionvalues.Add(BuildableStructure.Buildingtype.concretefactory, concretefactory);
		productionvalues.Add(BuildableStructure.Buildingtype.glassworks, glassworks);
		productionvalues.Add(BuildableStructure.Buildingtype.sawmill, sawmill);
		productionvalues.Add(BuildableStructure.Buildingtype.mine, mine);
		productionvalues.Add(BuildableStructure.Buildingtype.pumpjack, pumpjack);
		productionvalues.Add(BuildableStructure.Buildingtype.manufacture, manufacture);
		productionvalues.Add(BuildableStructure.Buildingtype.factory, factory);
		productionvalues.Add(BuildableStructure.Buildingtype.hightechfactory, hightechfactory);
		productionvalues.Add(BuildableStructure.Buildingtype.farm, farm);
		productionvalues.Add(BuildableStructure.Buildingtype.hydroponicfarm, hydroponicfarm);
		productionvalues.Add(BuildableStructure.Buildingtype.spaceport, spaceport);
		}

	void FixedUpdate()
		{
		if(++updatecounter >= updatefrequency)
			{
			foreach(BuildableStructure.Buildingtype buildingtype in Enum.GetValues(typeof(BuildableStructure.Buildingtype)))
				{
				foreach(Resourcetype resourcetype in Enum.GetValues(typeof(Resourcetype)))
					{
					resources[resourcetype] += productionvalues[buildingtype][resourceindices[resourcetype]] * buildingmanager.getBuildingCount(buildingtype);
					}
				}
			updatecounter = 0;
			}

		string resourcetext = "";
		foreach(Resourcetype type in Enum.GetValues(typeof(Resourcetype)))
			{
			resourcetext += resourcenames[type] + ": " + resources[type] + " ";
			if(projectcosts[resourceindices[type]] != 0)
				{
				resourcetext += "(" + projectcosts[resourceindices[type]] + ") ";
				}
			resourcetext += "\t\t\t\t";
			}
		resourcedisplay.text = resourcetext;
		}

	public bool subtractResources(int[] resources)
		{
		foreach(Resourcetype type in Enum.GetValues(typeof(Resourcetype)))
			{
			if(resources[resourceindices[type]] > this.resources[type])
				{
				return false;
				}
			}

		foreach(Resourcetype type in Enum.GetValues(typeof(Resourcetype)))
			{
			this.resources[type] -= resources[resourceindices[type]];
			}

		return true;
		}

	public void setProjectCosts(int[] projectcosts)
		{
		this.projectcosts = projectcosts;
		}

	public void unsetProjectCosts()
		{
		projectcosts = new int[5];
		for(int I = 0; I < 5; ++I)
			{
			projectcosts[I] = 0;
			}
		}
	}
