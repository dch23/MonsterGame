using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour {

	public float swayAmount = 1f;
	public float maxSwayAmount = 2f;
	public float smoothness = 1f;

	private Vector3 initialPosition;


	// Use this for initialization
	void Start () {
		initialPosition = transform.localPosition;
	}

	// Update is called once per frame
	void Update () {
		float movementX = -Input.GetAxis("Mouse X") * swayAmount;
		float movementY = -Input.GetAxis("Mouse Y") * swayAmount;

		movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
		movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);

		Vector3 finalPosition = new Vector3(movementX, movementY, 0);

		//only smooth out gun movement if not Aiming
		if (!Input.GetButton("Fire2")) {
			transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothness);
		}
		else {
			transform.localPosition = initialPosition;
		}
	}
}
