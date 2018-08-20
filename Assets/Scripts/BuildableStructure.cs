using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableStructure : MonoBehaviour
	{
	[SerializeField] Buildingtype type;
	[SerializeField] int startheight = 100;
	[SerializeField] string environmenttag = "Ground"; // TODO: rename groundtag
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
	private WarningManager warningmanager;

	private void FixedUpdate()
		{
		if(!placed)
			{
			// TODO: replace by own code
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit1;
			if(Physics.Raycast(ray, out hit1))
				{
				transform.position = new Vector3(Mathf.RoundToInt(hit1.point.x), startheight, Mathf.RoundToInt(hit1.point.z));
				}
			// TODO-end

			/* Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = new Vector3(mouseWorldPosition.x, startHeight, mouseWorldPosition.z); */

			// Snap to grid
			//transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));

			// Check terrain connection
			RaycastHit hit;
			Vector3 min = GetComponent<BoxCollider>().bounds.min;
			Vector3 max = GetComponent<BoxCollider>().bounds.max;
			Vector3[] corners = new Vector3[4];
			corners[0] = new Vector3(min.x, min.y, min.z);
			corners[1] = new Vector3(min.x, min.y, max.z);
			corners[2] = new Vector3(max.x, min.y, min.z);
			corners[3] = new Vector3(max.x, min.y, max.z);
			int[] distances = new int[corners.Length];
			bool solidground = true;

			for(int I = 0; I < corners.Length; ++I)
				{
				if(Physics.Raycast(corners[I], Vector3.down, out hit, 200))
					{
					if(hit.collider.gameObject.tag.Equals(environmenttag) || hit.collider.gameObject.tag.Equals(structuretag)) // TODO: if combining this with the upper if, Build (not in Editor) ignores the rest of Update() or does it now? No time to test
						{
						distances[I] = Mathf.RoundToInt(hit.distance);
						}
					else
						{
						distances[I] = -1;
						solidground = false;
						}
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
				if(Input.GetMouseButtonDown(0) && warningmanager != null)
					{
					if(!solidground)
						{
						warningmanager.setWarning("This is no flat ground"); // TODO: unique warning for placement on water
						}
					else if(collidingstructures > 0)
						{
						warningmanager.setWarning("Building collides with other buildings");
						}
					}
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

	public void setWarningManager(WarningManager warningmanager)
		{
		this.warningmanager = warningmanager;
		}
	}
