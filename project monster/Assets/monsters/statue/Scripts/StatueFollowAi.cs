using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueFollowAi : MonoBehaviour
{
    public Animator animator;

    public Transform player_transform;

    private float initialMoveSpeed = 10f;
    private float moveSpeed;

	private float initialRotSpeed = 20f;
	private float rotationSpeed;

    private float minRotSpeed = 0.01f;
    private float maxRotSpeed = 10f;

	private Quaternion rotationQuaternion;

    private float distance;

    private float followRadius = 20f;

    public Renderer renderer;

    private bool playerIsLooking;


    // Start is called before the first frame update
    void Start()
    {


        moveSpeed = initialMoveSpeed;
		rotationSpeed = initialRotSpeed;

        //renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        if (distance != Vector3.Distance(transform.position, player_transform.localPosition)) {
            distance = Vector3.Distance(transform.position, player_transform.localPosition);
        }

        if (renderer.isVisible) {
            playerIsLooking = true;
        }
        else {
            playerIsLooking = false;
        }

        if (playerIsLooking == true) {
            // Debug.Log("LOOKING");
            if (animator.GetBool("playerIsLooking") != true) {
                animator.SetBool("playerIsLooking", true);
            }
        }
        else {
            // Debug.Log("NOT LOOKING");
            if (animator.GetBool("playerIsLooking") != false) {
                animator.SetBool("playerIsLooking", false);
            }

            // rotationSpeed = Mathf.Clamp(-distance + followRadius+1f, minRotSpeed, maxRotSpeed); // if it is closer it will turn faster // it is follow radius + 1 to ensure the rotation speed is never negative
            //moveSpeed = Mathf.Clamp(-distance + followRadius+1f, minRotSpeed, maxRotSpeed+2f); // if it is closer it will turn faster // it is follow radius + 1 to ensure the rotation speed is never negative
            //rotate
            rotationQuaternion = Quaternion.Slerp(rotationQuaternion, Quaternion.LookRotation(player_transform.position - transform.position), rotationSpeed * Time.deltaTime);
            rotationQuaternion.Set(transform.rotation.x,rotationQuaternion.y,transform.rotation.z,rotationQuaternion.w); // change the rotation on only the y and w
            transform.rotation = rotationQuaternion;

            //Move
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        // Debug.Log(distance);






        // if (distance < followRadius) { //if it is in range then it can follow the player
        //
        //
        //
        //     // animator.SetBool("playerIsLooking", false);
        //
        //
        //     // //rotate
        //     // rotationQuaternion = Quaternion.Slerp(rotationQuaternion, Quaternion.LookRotation(player_transform.position - transform.position), rotationSpeed * Time.deltaTime);
        //     // rotationQuaternion.Set(transform.rotation.x,rotationQuaternion.y,transform.rotation.z,rotationQuaternion.w); // change the rotation on only the y and w
        //     // transform.rotation = rotationQuaternion;
        //     //
        //     // //Move
        //     // transform.position += transform.forward * moveSpeed * Time.deltaTime;
        // }
        // else {
        //     // animator.SetBool("playerIsLooking", true);
        // }
    }
}
