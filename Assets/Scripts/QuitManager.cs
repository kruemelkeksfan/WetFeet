using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
	{
	[SerializeField] Canvas menu;

	void Update()
		{
		if(Input.GetKeyDown(KeyCode.Escape))
			{
			menu.gameObject.SetActive(!menu.gameObject.activeSelf);
			}
		}

	public void quit()
		{
		Application.Quit();
		}
	}
