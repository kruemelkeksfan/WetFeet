using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
	{
	[SerializeField] CameraManager cameraManager;
	[SerializeField] RessourceManager ressourcemanager;

	// private List<GameObject> structures;
	private GameObject currentBuilding;
	private BuildableStructure currentBuildableStructure;

	private void Start()
		{
		// structures = new List<GameObject>();
		}

	void Update()
		{
		if(currentBuildableStructure != null) // while placing building
			{
			if(currentBuildableStructure.HasPlaced())// if building placed
				{
				cameraManager.AktivateMainCamera();
				currentBuildableStructure = null;
				addStructure(currentBuilding);
				}
			if(currentBuildableStructure != null && Input.GetKeyDown(KeyCode.R))
				{
				Vector3 currentRotation = currentBuilding.transform.rotation.eulerAngles;
				currentBuilding.transform.rotation = Quaternion.Euler(currentRotation + new Vector3(0, 90, 0));
				}
			if(Input.GetMouseButtonDown(1)) // if building cancelled
				{
				cameraManager.AktivateMainCamera();
				Object.Destroy(currentBuilding);
				}
			}
		}

	public void InstantiateBuilding(BuildableStructure building)
		{
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
