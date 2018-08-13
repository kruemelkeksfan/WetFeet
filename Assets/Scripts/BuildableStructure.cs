using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class BuildableStructure : MonoBehaviour
	{
	public const string environmentTag = "Environment";
	public const string structureTag = "Structure";

	[SerializeField] int startHeight = 0;

	private int collidingEnvironment = 0;
	private int collidingStructures = 0;
	private bool hasPlaced = false;

	private void FixedUpdate()
		{
		if(!hasPlaced)
			{
			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
			Vector3 mouseRPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = new Vector3(mouseRPosition.x, startHeight, mouseRPosition.z);
			transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)); // snap to grid
			GroundApproximation();
			if(Input.GetMouseButtonDown(0) && checkTerrainConnection() && collidingStructures == 0) // building requirements
				{
				// GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // TODO: Find bug which moves objects after placement // Question: Fixed?!
				transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
				hasPlaced = true;
				}
			else
				{
				// error/missing something notification
				}
			}

		if(collidingEnvironment < 0 || collidingStructures < 0)
			{
			print("Horrible explosion happening in BuildableStructure.cs!");
			print("collidingEnvironment == " + collidingEnvironment);
			print("collidingStructures == " + collidingStructures);
			}
		}

	private void GroundApproximation()
		{
		if(collidingEnvironment == 0)
			{
			transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
			}
		if(collidingEnvironment > 0)
			{
			transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
			}
		startHeight = Mathf.RoundToInt(transform.position.y);
		}

	private bool checkTerrainConnection()
		{
		return true;
		}

	private void OnTriggerEnter(Collider other)
		{
		if(other.tag == environmentTag)
			{
			collidingEnvironment = 1;
			}
		else if(other.tag == structureTag)
			{
			++collidingStructures;
			}
		}

	private void OnTriggerExit(Collider other)
		{
		if(other.tag == environmentTag)
			{
			print("call");
			--collidingEnvironment;
			print(collidingEnvironment);
			}
		else if(other.tag == structureTag)
			{
			--collidingStructures;
			}
		}

	public bool HasPlaced()
		{
		return hasPlaced;
		}
	}
