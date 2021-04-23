using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeThrower : MonoBehaviour {

	public float throwForce = 40f;
	public GameObject grenadePrefab;
	private GameObject grenade;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			ThrowGrenade();
		}
	}

	void ThrowGrenade () {
	//	grenade = Instantiate(grenadePrefab, Camera.main.transform.localPosition, Camera.main.transform.rotation);
	//	Rigidbody rb = grenade.GetComponent<RigidBody>();
	//	rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
	}
}
