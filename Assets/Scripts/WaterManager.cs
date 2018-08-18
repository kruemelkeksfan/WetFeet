using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
	{
	[SerializeField] GameObject topwater;
	[SerializeField] float transitionspeed = 0.02f;
	[SerializeField] float wavespeed = 0.02f;
	[SerializeField] float maxwaveheight = 2;
	[SerializeField] int terrainheight = 5;

	private float oldsealevel = 0;
	private float sealevel = 0;
	private float transitionprogress = 0;
	private float lowerbound = 0;
	private float wavelevel = 0;
	private float lastvalidwave = 0;
	private float waveprogress = 0;

	private void Start()
		{
		lowerbound = topwater.transform.position.y;
		}

	private void FixedUpdate()
		{
		// Generate waves
		float waterlevel;
		if(transitionprogress >= 1)
			{
			waterlevel = sealevel + (topwater.transform.position.y - transform.position.y);
			}
		else
			{
			waterlevel = Mathf.Lerp(oldsealevel, sealevel, transitionprogress) + (topwater.transform.position.y - transform.position.y);
			}
		float [] waveheights = new float [3];
		waveheights[0] = (Mathf.FloorToInt(waterlevel) % terrainheight) + (waterlevel - Mathf.FloorToInt(waterlevel)) - 0.2f;
		waveheights[1] = (Mathf.FloorToInt(waterlevel) / terrainheight + 1) * terrainheight - waterlevel - 0.2f;
		waveheights[2] = maxwaveheight;
		for(int I = 0; I < waveheights.Length; ++I)
			{
			if(waveheights[I] < 0)
				{
				waveheights[I] = 0;
				}
			}
		wavelevel = Mathf.Sin(waveprogress) * Mathf.Min(waveheights);

		if(transitionprogress >= 1)
			{
			transform.position = new Vector3(transform.position.x, sealevel + wavelevel, transform.position.z);
			}
		else
			{
			transform.position = new Vector3(transform.position.x, Mathf.Lerp(oldsealevel, sealevel, transitionprogress) + wavelevel, transform.position.z);
			transitionprogress += transitionspeed;
			}

		waveprogress += wavespeed;
		if(waveprogress > 2 * Mathf.PI)
			{
			waveprogress -= 2 * Mathf.PI;
			}
		}

	public float getSealevel()
		{
		return sealevel;
		}

	public void setSealevel(float sealevel)
		{
		if(transitionprogress >= 1)
			{
			oldsealevel = this.sealevel;
			}
		else
			{
			oldsealevel =  Mathf.Lerp(oldsealevel, sealevel, transitionprogress);
			}

		transitionprogress = 0;
		this.sealevel = sealevel;
		}
	}
