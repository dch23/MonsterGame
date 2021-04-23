using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderFollowAI : MonoBehaviour
{
    Transform player_transform;
    private float initialMoveSpeed = 5f;
	private float initialRotSpeed = 3f;
	private float rotationSpeed;
	private float moveSpeed;
	// public float followRadius = 10f;
	// private float stopRadius = 2f;
	// private float distance;
	private Quaternion rotationQuaternion;


    public Spider spider;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start() {

        player_transform = GameObject.FindWithTag("Player").transform;

        moveSpeed = initialMoveSpeed;
		rotationSpeed = initialRotSpeed;


        //direction = new Vector3(0f,0.0f,0.01f);

        //rotationQuaternion = Quaternion.Set(0f,0.01f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        //spider.setGroundcheck(!Input.GetKey(KeyCode.Space));
        //
        Vector3 lastPosition = transform.position;

        //transform.position += direction;
        // if (lastPosition != transform.position) {
        //     spider.isWalking = true;
        // }

        // // transform.rotation = Quaternion.Set
        // spider.isWalking = true;

        rotationQuaternion = Quaternion.Slerp(rotationQuaternion, Quaternion.LookRotation(player_transform.position - transform.position), rotationSpeed * Time.deltaTime);
        rotationQuaternion.Set(transform.rotation.x,rotationQuaternion.y,transform.rotation.z,rotationQuaternion.w); // change the rotation on only the y and w
        transform.rotation = rotationQuaternion;

        //Move
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
