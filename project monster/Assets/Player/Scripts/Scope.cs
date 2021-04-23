using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Scope : MonoBehaviour {

	public GameObject scope;
    public float scopeTimeSpreadValue = 1f;

    public GameObject WeaponsCamera;
    public Camera mainCamera;
    public float ScopeFOV = 15f;
    private float normalFOV;

    //public GameObject player;
	public FirstPersonController playerScript;
	//public WeaponSwitching weaponSwitchingScript;
    //MouseLook mouseScript;

    private float initial_XSensitivity;
    private float initial_YSensitivity;
    public float scope_XSensitivity = 1f;
    public float scope_YSensitivity = 1f;



    Vector3 aimVector;
    Vector3 scopeTimeSpread;

    void Start () {
        //normalFOV = mainCamera.fieldOfView;
        scopeTimeSpread = new Vector3(scopeTimeSpreadValue,scopeTimeSpreadValue,scopeTimeSpreadValue);

		//initial_XSensitivity = playerScript.m_XSensitivity;
	//	initial_YSensitivity = playerScript.m_YSensitivity;
        //mouseScript = GetComponent<MouseLook>();
        //initial_XSensitivity = mouseScript.XSensitivity;
    //  ControllerScript.m_MouseLook.XSensitivity = scope_YSensitivity;


    }

	void Update () {

        aimVector = new Vector3(-0.026f,-0.16715f,0.57f);

		//int selectedWeapon = weaponSwitchingScript.selectedWeapon;
		//int previousSelectedWeapon = weaponSwitchingScript.previousSelectedWeapon;

        //this very long if statement checks if the transform.position is in a box i created with < and > statements
		if ((Input.GetButton("Fire2")) && (transform.localPosition.x > (aimVector.x - scopeTimeSpread.x)) && (transform.localPosition.x < (aimVector.x + scopeTimeSpread.x)) && (transform.localPosition.y > (aimVector.y - scopeTimeSpread.y)) && (transform.localPosition.y < (aimVector.y + scopeTimeSpread.y)) && (transform.localPosition.z > (aimVector.z - scopeTimeSpread.z)) && (transform.localPosition.z < (aimVector.z + scopeTimeSpread.z))) {
            scope.SetActive(true);
            WeaponsCamera.SetActive(false);

            mainCamera.fieldOfView = ScopeFOV;
			//change Sensitivity
			playerScript.m_XSensitivity = scope_XSensitivity;
			playerScript.m_YSensitivity = scope_YSensitivity;

		}
		/*if ((!Input.GetButton("Fire2")) || gameObject.activeInHierarchy != true) {
			WeaponsCamera.SetActive(true);

            mainCamera.fieldOfView = normalFOV;

			//change sensitivity back to initial Sensitivity
			playerScript.m_XSensitivity = initial_XSensitivity;
			playerScript.m_YSensitivity = initial_YSensitivity;

			scope.SetActive(false);
		}*/
	}
}
