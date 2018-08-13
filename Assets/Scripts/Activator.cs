using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
	{
	[SerializeField] bool startstate;
	[SerializeField] List<GameObject> activatables;

	public void changeState()
		{
		if(startstate)
			{
			deactivate();
			}
		else
			{
			activate();
			}
		startstate = !startstate;
		}

	public void activate()
		{
		foreach(GameObject activatable in activatables)
			{
			activatable.SetActive(true);
			}
		startstate = true;
		}

	public void deactivate()
		{
		foreach(GameObject activatable in activatables)
			{
			activatable.SetActive(false);
			}
		startstate = false;
		}
	}
