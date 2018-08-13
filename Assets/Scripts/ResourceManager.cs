using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
	{
	[SerializeField] int[] resources;
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
	[SerializeField] Text resourcedisplay;
	[SerializeField] int updatefrequency = 50;


	public const int WORKFORCE = 0;
	public const int BUILDING_MATERIALS = 1;
	public const int RESOURCES = 2;
	public const int GOODS = 3;
	public const int FOOD = 4;

	private Dictionary<BuildableStructure.Buildingtype, int[]> productionvalues;
	private Dictionary<BuildableStructure.Buildingtype, int> structurecounts; // TODO: move to building manager
	private int updatecounter = 0;

	private void Start()
		{
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

		structurecounts = new Dictionary<BuildableStructure.Buildingtype, int>();
		}

	void FixedUpdate()
		{
		// +49 800 1810771
		if(++updatecounter >= updatefrequency)
			{
			foreach(BuildableStructure.Buildingtype structure in structurecounts.Keys)
				{
				resources[WORKFORCE] += (productionvalues[structure][WORKFORCE]) * structurecounts[structure];
				resources[BUILDING_MATERIALS] += (productionvalues[structure][BUILDING_MATERIALS]) * structurecounts[structure];
				resources[RESOURCES] += (productionvalues[structure][RESOURCES]) * structurecounts[structure];
				resources[GOODS] += (productionvalues[structure][GOODS]) * structurecounts[structure];
				resources[FOOD] += (productionvalues[structure][FOOD]) * structurecounts[structure];
				}

			resourcedisplay.text = "Workforce: " + resources[WORKFORCE]
				+ "\t\t\t\t Building Materials: " + resources[BUILDING_MATERIALS]
				+ "\t\t\t\t Resources: " + resources[RESOURCES]
				+ "\t\t\t\t Goods: " + resources[GOODS]
				+ "\t\t\t\t Food: " + resources[FOOD];

			updatecounter = 0;
			}
		}

	public bool subtractRessources(int[] resources)
		{
		if(this.resources[WORKFORCE] >= resources[WORKFORCE]
			&& this.resources[BUILDING_MATERIALS] >= resources[BUILDING_MATERIALS]
			&& this.resources[RESOURCES] >= resources[RESOURCES]
			&& this.resources[GOODS] >= resources[GOODS]
			&& this.resources[FOOD] >= resources[FOOD])
			{
			this.resources[WORKFORCE] -= resources[WORKFORCE];
			this.resources[BUILDING_MATERIALS] -= resources[BUILDING_MATERIALS];
			this.resources[RESOURCES] -= resources[RESOURCES];
			this.resources[GOODS] -= resources[GOODS];
			this.resources[FOOD] -= resources[FOOD];

			return true;
			}
		else
			{
			return false;
			}
		}

	public void addStructure(BuildableStructure.Buildingtype structure)
		{
		if(structurecounts.ContainsKey(structure))
			{
			++structurecounts[structure];
			}
		else
			{
			structurecounts.Add(structure, 1);
			}
		}

	public void destroyStructure(BuildableStructure.Buildingtype structure)
		{
		if(structurecounts.ContainsKey(structure) && structurecounts[structure] > 0)
			{
			--structurecounts[structure];
			}
		else
			{
			print("Horrible explosion happening in RessourceManager.cs!");
			print(structure);
			}
		}

	public  Dictionary<BuildableStructure.Buildingtype, int> getStructureCounts()
		{
		return structurecounts;
		}
	}
