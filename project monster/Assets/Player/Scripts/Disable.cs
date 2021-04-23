using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Disable : MonoBehaviour {

	private float initial_XSensitivity;
	private float initial_YSensitivity;
	//public float scope_XSensitivity = 1f;
	//public float scope_YSensitivity = 1f;

	public float ScopeFOV = 15f;
	private float normalFOV;

	public GameObject SniperRifle;
	public FirstPersonController playerScript;
	public Scope scopeScript;
	public GameObject WeaponsCamera;

	//public float scopeTimeSpreadValue = 1f;
	//Vector3 scopeTimeSpread;
	//Vector3 aimVector;
	//public Scope ScopeScript;


	// Use this for initialization
	void Start () {
		initial_XSensitivity = 2f;
		initial_YSensitivity = 2f;

		normalFOV = 60f;

		//scopeTimeSpread = new Vector3(scopeTimeSpreadValue,scopeTimeSpreadValue,scopeTimeSpreadValue);
	}

	// Update is called once per frame
	void Update () {

		//aimVector = new Vector3(-0.026f,-0.16715f,0.57f);

		if ((!Input.GetButton("Fire2")) || SniperRifle.activeInHierarchy == false) {
			WeaponsCamera.SetActive(true);

            Camera.main.fieldOfView = normalFOV;

			//change sensitivity back to initial Sensitivity
			playerScript.m_XSensitivity = initial_XSensitivity;
			playerScript.m_YSensitivity = initial_YSensitivity;

			gameObject.SetActive(false);
		}
	}
}
