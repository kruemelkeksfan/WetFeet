using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningManager : MonoBehaviour
	{
	[SerializeField] Text warningtext;
	[SerializeField] float displaytime = 5;

	private float warningtime = 0;
	
	void FixedUpdate ()
		{
		if(warningtime <= 0)
			{
			warningtext.text = "";
			}
		else
			{
			warningtime -= Time.deltaTime;
			}
		}

	public void setWarning(string warning)
		{
		warningtext.text = warning;
		warningtime = displaytime;
		}
	}
