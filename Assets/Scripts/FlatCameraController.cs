using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatCameraController : MonoBehaviour
	{
	[SerializeField] int speed = 2; // TODO: Setter?
	[SerializeField] HighlightManager highlighter;

	private float pitch = 0;
	private float yaw = 0;
	private float roll = 0;
	private float currentspeed = 0;
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
		}

	void Update()
		{
		// Quit on Escape
		if(Input.GetKeyDown(KeyCode.Escape))
			{
			Application.Quit();
			}

		// Boost on Shift
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
			currentspeed = speed * 5;
			}
		else
			{
			currentspeed = speed;
			}

		// Hide and lock cursor on MMB down
		if(Input.GetMouseButtonDown(2))
			{
			Cursor.lockState = CursorLockMode.Locked;
			}

		// Show and unlock cursor on MMB release
		if(Input.GetMouseButtonUp(2))
			{
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
		transform.position += (Quaternion.Euler(pitch, yaw, roll) * direction) * currentspeed;
		transform.rotation = Quaternion.Euler(pitch, yaw, roll);
		}
	}
