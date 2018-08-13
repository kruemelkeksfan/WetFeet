using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class BuildableStructure : MonoBehaviour
	{
	public const string environmentTag = "Environment";
	public const string structureTag = "Structure";

	public enum Buildingtype
		{
		smallhouse,
		apartmentblock,
		skyscraper,
		mason,
		concretefactory,
		glassworks,
		sawmill,
		mine,
		pumpjack,
		manufacture,
		factory,
		hightechfactory,
		farm,
		hydroponicfarm,
		spaceport
		}

	[SerializeField] Buildingtype type;
	[SerializeField] int startHeight = 100;

	private int collidingStructures = 0;
	private bool hasPlaced = false;

	private void FixedUpdate()
		{
		if(!hasPlaced)
			{
			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
			Vector3 mouseRPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = new Vector3(mouseRPosition.x, startHeight, mouseRPosition.z);

			// Snap to grid
			transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));

			// Check terrain connection
			RaycastHit hit;
			Vector3 min = GetComponent<BoxCollider>().bounds.min;
			Vector3 max = GetComponent<BoxCollider>().bounds.max;
			Vector3 [] corners = new Vector3 [4];
			corners [0] = new Vector3(min.x, min.y, min.z);
			corners [1] = new Vector3(min.x, min.y, max.z);
			corners [2] = new Vector3(max.x, min.y, min.z);
			corners [3] = new Vector3(max.x, min.y, max.z);
			int [] distances = new int [corners.Length];
			bool solidground = true;

			for(int I = 0; I < corners.Length; ++I)
				{
				if(Physics.Raycast(corners[I], Vector3.down, out hit, 200))
					{
					distances[I] = Mathf.RoundToInt(hit.distance);
					}
				else
					{
					distances[I] = -1;
					solidground = false;
					}
				}

			for(int I = 0; I < distances.Length; ++I)
				{
				if(distances[I] != distances[(I + 1) % distances.Length])
					{
					solidground = false;
					}
				}

			//Snap to ground
			if(Physics.Raycast(transform.position, Vector3.down, out hit, 200))
				{
				transform.position = hit.point;
				}

			// Check building requirements
			if(Input.GetMouseButtonDown(0) && solidground && collidingStructures == 0)
				{
				transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
				hasPlaced = true;
				}
			else
				{
				// error/missing something notification
				}
			}

		if(collidingStructures < 0)
			{
			print("Horrible explosion happening in BuildableStructure.cs!");
			print("collidingStructures == " + collidingStructures);
			}
		}

	private void OnTriggerEnter(Collider other)
		{
		if(other.tag == structureTag)
			{
			++collidingStructures;
			}
		}

	private void OnTriggerExit(Collider other)
		{
		if(other.tag == structureTag)
			{
			--collidingStructures;
			}
		}

	public Buildingtype getBuildingType()
		{
		return type;
		}

	public bool HasPlaced()
		{
		return hasPlaced;
		}
	}
