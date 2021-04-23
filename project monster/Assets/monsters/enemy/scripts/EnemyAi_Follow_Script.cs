using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class EnemyAi_Follow_Script : MonoBehaviour {

	//Animation
	public Animator animator;

	//rigid body
	private Rigidbody rb;
	//
	//Jump
	private bool isGrounded = false;
	private float jump_force = 0.8f;
	private float jump_forward_force = 1.5f;
	//

	public Transform player_transform;

	private float initialMoveSpeed = 7f;
	public float initialRotSpeed = 3f;
	private float rotationSpeed;
	private float moveSpeed;
	public float followRadius = 10f;
	private float stopRadius = 2f;
	private float distance;
	private Quaternion rotationQuaternion;

	private float detectSelfReach = 5f;

	private float stopPlayerRayDistance = 2f;

	private float randomRot;
	private Quaternion randomRotQuaternion;
	private bool isRandomTurning = false;



	void Start () {
		moveSpeed = initialMoveSpeed;
		rotationSpeed = initialRotSpeed;
		rb = GetComponent<Rigidbody>(); //fetch rigid body
	}
	//isGrounded
	void OnCollisionEnter(Collision groundCollision) {
		if (groundCollision.gameObject.tag == "Solid") {
			isGrounded = true;
		}
	}
	void OnCollisionExit (Collision groundCollision) {
		if (groundCollision.gameObject.tag != "Solid") {
			isGrounded = false;
		}
	}

	// Update is called once per frame
	void Update () {

		//Animation
		if (moveSpeed > 0) {
			animator.SetBool("Run",true);
		}
		else {
			animator.SetBool("Run",false);
		}
		//
		RaycastHit hit;
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		bool forwardRay = Physics.Raycast(transform.position,forward,out hit,detectSelfReach);
		if (forwardRay == true) {
			/*
			if (hit.transform.gameObject.tag == "Enemy1") {
				randomRot = Random.Range(-20f,20f);
				randomRotQuaternion.Set(0,rotationQuaternion.y + randomRot,0,rotationQuaternion.w);
				isRandomTurning = true;
			}
			*/
			if (hit.transform.gameObject.tag == "Enemy1" && moveSpeed > 0) {
				//Debug.Log("hit other enemy"); //debug
				if (isGrounded == true) {

					rb.AddForce(Vector3.up*jump_force,ForceMode.Impulse); // jump
					rb.AddForce(transform.forward * jump_forward_force,ForceMode.Impulse);
				}
			}
		}
		/*
		RaycastHit hit2;

		//if the player front ray is hit then stop the dog.
		float temp_y = transform.localPosition.y;
		float temp_x = transform.localPosition.x;
		float temp_z = transform.localPosition.z;

		Vector3 firstRayPosition = new Vector3(temp_x + 0.2f, temp_y + 1f, temp_z + 1f);
		Vector3 secondRayPosition = new Vector3(temp_x - 0.2f, temp_y + 1f, temp_z + 1f);

		//Debug.DrawRay(firstRayPosition, forward); Debug.DrawRay(secondRayPosition, forward);

		//bool forwardRay = Physics.Raycast(transform.localPosition,forward,out hit, attackRadius);
		bool ray1 = Physics.Raycast(firstRayPosition,forward,out hit2, stopPlayerRayDistance);
		bool ray2 = Physics.Raycast(secondRayPosition,forward,out hit2, stopPlayerRayDistance);


		if (ray1 || ray2) {
			if (hit2.transform.name == "Player") {
				moveSpeed = 0;
			}
		}*/

		bool playerRay = Physics.Raycast(transform.position,forward,out hit,stopPlayerRayDistance);

		if (playerRay == true) {
			if (hit.transform.gameObject.tag == "Player") {
				moveSpeed = 0;
			}
		}


		//check if another raycast hits player

		//
		//raycast
		//Debug.DrawLine(transform.localPosition,transform.forwardDir);

		Debug.DrawRay(transform.position, forward, Color.red);

		distance = Vector3.Distance(transform.position, player_transform.localPosition);
	//	Debug.Log(transform.forward);

		//look at player only on y axis rotation
		rotationQuaternion = Quaternion.Slerp(rotationQuaternion, Quaternion.LookRotation(player_transform.position - transform.position), rotationSpeed * Time.deltaTime);
		rotationQuaternion.Set(0,rotationQuaternion.y,0,rotationQuaternion.w);
		transform.localRotation = rotationQuaternion;

		//move to Player
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		//only run if the distance is less or equal to radius
		if (distance <= followRadius && distance > stopRadius) {
			moveSpeed = initialMoveSpeed;
			rotationSpeed = initialRotSpeed;
		}
		else {
			moveSpeed = 0;
			//rotationSpeed = 0;
		}
		if (distance > followRadius) {
			rotationSpeed = 0;
		}
	}
	/*
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Enemy1") {
			Destroy(gameObject);
		}
	}
	*/
}
