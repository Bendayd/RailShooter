using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[Tooltip("En m/s")][SerializeField] float xSpeed = 20f;
	[Tooltip("En m/s")][SerializeField] float ySpeed = 20f;
	[SerializeField] float xRange = 8f;
	[SerializeField] float yMin = -6f;
	[SerializeField] float yMax = 6f;

	[SerializeField] float positionPitchFactor = -2.75f;
	[SerializeField] float positionYawFactor = 2.75f;
	[SerializeField] float controlPitchFactor = -10f;
	[SerializeField] float controlRollFactor = -10f;

	float xThrow, yThrow;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessTranslation();
		ProcessRotation();
	}

	private void ProcessTranslation()
	{
		xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		yThrow = CrossPlatformInputManager.GetAxis("Vertical");

		float xOffset = xThrow * xSpeed * Time.deltaTime; //This frame
		float yOffset = yThrow * ySpeed * Time.deltaTime;

		float rawXPos = transform.localPosition.x + xOffset;
		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

		float rawYPos = transform.localPosition.y + yOffset;
		float clampedYPos = Mathf.Clamp(rawYPos, yMin, yMax);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}

	private void ProcessRotation()
	{
		float pitchPosition = transform.localPosition.y * positionPitchFactor;
		float pitchControl = yThrow * controlPitchFactor;
		float pitch = pitchPosition + pitchControl;

		float yaw = transform.localPosition.x * positionYawFactor;

		float rollLerp = Mathf.Lerp(controlRollFactor, -controlRollFactor, Mathf.InverseLerp(-xRange, xRange, transform.localPosition.x));
		float rollClampMin = Mathf.Clamp(rollLerp - -controlRollFactor, controlRollFactor, 0);
		float rollClampMax = Mathf.Clamp(rollLerp + -controlRollFactor, 0 , -controlRollFactor);
		float roll = Mathf.Clamp(xThrow * controlRollFactor, rollClampMin, rollClampMax);

		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}
}
