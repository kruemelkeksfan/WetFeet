using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class CameraManager : MonoBehaviour
	{
	[SerializeField] SimpleCameraController mainCamera;
	[SerializeField] FlatCameraController buildingCamera;
	[SerializeField] GameObject scriptcontainer;

	private  HighlightManager highlighter;

	private void Start()
		{
		highlighter = scriptcontainer.GetComponent<HighlightManager>();
		}

	void Update()
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
		highlighter.disableHighlight();
		}

	public void AktivateBuildingCamera()
		{
		mainCamera.gameObject.SetActive(false);
		buildingCamera.gameObject.SetActive(true);
		highlighter.enableHighlight();
		}
	}
