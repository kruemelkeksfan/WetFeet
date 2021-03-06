﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
	{
	[SerializeField] float speed = 0;
	[SerializeField] float zoomspeed = 0;
	[SerializeField] HighlightManager highlighter;
	[SerializeField] Slider speedslider;
	[SerializeField] Slider zoomslider;

	private float pitch = 0;
	private float yaw = 0;
	private float roll = 0;
	private float currentspeed = 0;
	private float currentzoomspeed = 0;
	private float zoom = 0;
	private bool topdown = false;
	private Vector3 oldposition = Vector3.zero;
	private float oldpitch = 0;
	private float oldyaw = 0;
	private float oldroll = 0;

	private void Start()
		{
		Vector3 startrotation = transform.rotation.eulerAngles;

		pitch = startrotation.x;
		yaw = startrotation.y;
		roll = startrotation.z;

		updateSpeed();
		updateZoom();
		}

	void Update()
		{
		// Boost on Shift
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
			currentspeed = speed * 5;
			currentzoomspeed = zoomspeed * 5;
			}
		else
			{
			currentspeed = speed;
			currentzoomspeed = zoomspeed;
			}

		// Hide and lock cursor on MMB down
		if(Input.GetMouseButtonDown(2))
			{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			}

		// Show and unlock cursor on MMB release
		if(Input.GetMouseButtonUp(2))
			{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			}

		// Rotation
		if(Input.GetMouseButton(2))
			{
			Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

			yaw += mouseMovement.x;
			pitch -= mouseMovement.y;
			}

		// Get input directions
		Vector3 direction = new Vector3();
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
			direction += Vector3.forward;
			}
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
			direction += Vector3.left;
			}
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
			direction += Vector3.back;
			}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
			direction += Vector3.right;
			}
		if(Input.GetKey(KeyCode.Space))
			{
			direction += Vector3.up;
			}
		if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
			direction += Vector3.down;
			}

		// Switch to top-down on Alt
		if(Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
			{
			if(!topdown)
				{
				highlighter.enableHighlight();

				oldposition = transform.position;
				oldpitch = pitch;
				oldyaw = yaw;
				oldroll = roll;

				RaycastHit hit;
				if(Physics.Raycast(transform.position, transform.forward, out hit, 1000))
					{
					transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
					}
				pitch = 90;

				topdown = true;
				}
			else
				{
				highlighter.disableHighlight();

				transform.position = oldposition;
				pitch = oldpitch;
				yaw = oldyaw;
				roll = oldroll;

				topdown = false;
				}
			}

		// Translate and rotate camera
		transform.position += (Quaternion.Euler(0, yaw, 0) * direction) * currentspeed;
		transform.position += (Quaternion.Euler(pitch, yaw, roll) * Vector3.forward) * Input.GetAxis("Mouse ScrollWheel") * currentzoomspeed; // Zoom with ScrollWheel
		transform.rotation = Quaternion.Euler(pitch, yaw, roll);
		}

	public void updateSpeed()
		{
		if(speedslider != null && speedslider.gameObject.activeSelf)
			{
			speed = speedslider.value;
			}
		}

	public void updateZoom()
		{
		if(zoomslider != null && zoomslider.gameObject.activeSelf)
			{
			zoomspeed = zoomslider.value;
			}
		}
	}
