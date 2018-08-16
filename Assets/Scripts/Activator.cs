using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
	{
	[SerializeField] List<GameObject> activatables;

	public void changeState()
		{
		if(activatables[0].activeSelf)
			{
			deactivate();
			}
		else
			{
			activate();
			}
		}

	public void activate()
		{
		foreach(GameObject activatable in activatables)
			{
			if(!activatable.activeSelf)
				{
				activatable.SetActive(true);
				}
			}
		}

	public void deactivate()
		{
		foreach(GameObject activatable in activatables)
			{
			if(activatable.activeSelf)
				{
				activatable.SetActive(false);
				}
			}
		}
	}
