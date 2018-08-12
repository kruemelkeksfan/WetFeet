using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] CameraManager cameraManager;

	GameObject currentBuilding;
    BuildableStructure currentBuildableStructure;

	void Update ()
    {
        if (currentBuildableStructure != null) // while placing building
        {
            if (currentBuildableStructure.HasPlaced)// if building placed
            {
                cameraManager.AktivateMainCamera();
                currentBuildableStructure = null;
            }
            if (currentBuildableStructure != null && Input.GetKeyDown(KeyCode.R))
            {
                Vector3 currentRotation = currentBuildableStructure.transform.rotation.eulerAngles;
                currentBuildableStructure.transform.rotation = Quaternion.Euler(currentRotation + new Vector3(0, 90, 0));
            }
            if (Input.GetMouseButtonDown(1)) // if building cancelled
            {
                cameraManager.AktivateMainCamera();
                Object.Destroy(currentBuilding);
            }
        }
	}
    public void InsatciateBuilding(BuildableStructure building)
    {
        if (Buildable())
        {
            cameraManager.AktivateBuildingCamera();
            currentBuilding = Instantiate(building.gameObject, transform.position, Quaternion.identity);
            currentBuildableStructure = currentBuilding.GetComponent<BuildableStructure>();
        }
        else
        {
            // error/missing something notification 
        }
    }
    bool Buildable() //building requirements
    {
        return true;
    }
}
