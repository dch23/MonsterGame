using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour {

	public float delay = 3f;
	public float blastRadius = 5f;
	public float explosionForce = 700f;
	bool hasExploded = false;

	public GameObject explosionEffect;

	float countdown;
	// Use this for initialization
	void Start () {
		countdown = delay;
	}

	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if (countdown <= 0f && !hasExploded) {
			Explode();
			hasExploded = true;
		}
	}
	void Explode () {
		Debug.Log("Exploded");
		//show effect
		Instantiate(explosionEffect,transform.position, transform.rotation);
		//get nearby objects
		Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

		foreach (Collider nearbyObjects in colliders) {
			Rigidbody rb = nearbyObjects.GetComponent<Rigidbody>();
			if (rb != null) {
				rb.AddExplosionForce(explosionForce,transform.position,blastRadius);
			}
		}
			//add force
			//damage

		//remove grenade
		Destroy(gameObject);
	}
}
