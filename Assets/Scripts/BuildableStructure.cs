using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableStructure : MonoBehaviour
	{
	[SerializeField] Buildingtype type;
	[SerializeField] int startHeight = 100;
	[SerializeField] string environmenttag = "Environment";
	[SerializeField] string structuretag = "Structure";

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

	private int collidingstructures = 0;
	private bool placed = false;

	private void FixedUpdate()
		{
		if(!placed)
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
				if(Physics.Raycast(corners[I], Vector3.down, out hit, 200) && hit.collider.tag.Equals(environmenttag))
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
			if(Input.GetMouseButtonDown(0) && solidground && collidingstructures == 0) // TODO: Input.GetMouseButtonDown(0) seems to have delay, can we use events? On the other hand sometimes this is called before old building is cancelled when somebody presses another button
				{
				transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
				placed = true;
				}
			else
				{
				// error/missing something notification
				}
			}

		if(collidingstructures < 0)
			{
			print("Horrible explosion happening in BuildableStructure.cs!");
			print("collidingStructures == " + collidingstructures);
			}
		}

	private void OnTriggerEnter(Collider other)
		{
		if(other.tag.Equals(structuretag))
			{
			++collidingstructures;
			}
		}

	private void OnTriggerExit(Collider other)
		{
		if(other.tag.Equals(structuretag))
			{
			--collidingstructures;
			}
		}

	public Buildingtype getBuildingType()
		{
		return type;
		}

	public bool hasPlaced()
		{
		return placed;
		}
	}
