using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
	{
	[SerializeField] CameraManager cameraManager;
	[SerializeField] RessourceManager ressourcemanager;
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

	// private List<GameObject> structures;
	private Dictionary<BuildableStructure.Buildingtype, int[]> buildcosts;
	private GameObject currentBuilding;
	private BuildableStructure currentBuildableStructure;

	private void Start()
		{
		// structures = new List<GameObject>();
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
		if(currentBuildableStructure != null) // while placing building
			{
			if(currentBuildableStructure.HasPlaced()) // if building placed
				{
				if(ressourcemanager.subtractRessources(buildcosts[currentBuildableStructure.getBuildingType()]))
					{
					cameraManager.AktivateMainCamera();
					addStructure(currentBuilding);
					currentBuilding = null;
					currentBuildableStructure = null;
					}
				else
					{
					cancelBuilding();
					// No Ressources Error
					}
				}
			if(currentBuildableStructure != null && Input.GetKeyDown(KeyCode.R))
				{
				Vector3 currentRotation = currentBuilding.transform.rotation.eulerAngles;
				currentBuilding.transform.rotation = Quaternion.Euler(currentRotation + new Vector3(0, 90, 0));
				}
			if(Input.GetMouseButtonDown(1)) // if building cancelled
				{
				cancelBuilding();
				}
			}
		}

	public void cancelBuilding()
		{
		if(currentBuilding != null && currentBuildableStructure != null)
			{
			cameraManager.AktivateMainCamera();
			Object.Destroy(currentBuilding);
			currentBuilding = null;
			currentBuildableStructure = null;
			}
		}

	public void InstantiateBuilding(BuildableStructure building)
		{
		cancelBuilding();

		cameraManager.AktivateBuildingCamera();
		currentBuilding = Instantiate(building.gameObject, transform.position, Quaternion.identity);
		currentBuildableStructure = currentBuilding.GetComponent<BuildableStructure>();
		}

	public void addStructure(GameObject structure)
		{
		// structures.Add(structure);
		ressourcemanager.addStructure(structure.GetComponent<BuildableStructure>().getBuildingType());
		}

	public void destroyStructure(GameObject structure)
		{
		// structures.Remove(structure);
		ressourcemanager.destroyStructure(structure.GetComponent<BuildableStructure>().getBuildingType());
		Object.Destroy(structure);
		}
	}
