using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
	{
	[SerializeField] GameObject island;
	[SerializeField] Material normalmaterial;
	[SerializeField] Material highlightedmaterial;

	public void enableHighlight()
		{
		island.GetComponent<Renderer>().material = highlightedmaterial;
		}

	public void disableHighlight()
		{
		island.GetComponent<Renderer>().material = normalmaterial;
		}
	}
