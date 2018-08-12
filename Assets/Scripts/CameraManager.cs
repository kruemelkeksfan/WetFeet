using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class CameraManager : MonoBehaviour {

    [SerializeField] SimpleCameraController mainCamera;
    [SerializeField] FlatCameraControlls buildingCamera;

	void Update ()
    {
        transform.position += mainCamera.transform.localPosition;
        transform.position += buildingCamera.transform.localPosition;
        mainCamera.transform.localPosition = new Vector3(0, 0, 0);
        buildingCamera.transform.localPosition = new Vector3(0, 0, 0);
	}
    public void AktivateMainCamera()
    {
        buildingCamera.gameObject.SetActive(false);

        mainCamera.gameObject.SetActive(true);
    }
    public void AktivateBuildingCamera()
    {
		print("call");

        mainCamera.gameObject.SetActive(false);

        buildingCamera.gameObject.SetActive(true);
    }
}
