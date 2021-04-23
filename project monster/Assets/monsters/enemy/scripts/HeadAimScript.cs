using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class HeadAimScript : MonoBehaviour
{
    public Transform headBone;
    //public GameObject headAim;
    public Transform player_transform;
    private float maxDistance = 20f;
    private float distance;

    private float initialRotSpeed = 30f;
	private float rotationSpeed;

    private float initialMoveSpeed = 0.1f;
    private float moveSpeed;

    private float y_aim_correction = 2.2f;

    private Quaternion rotationQuaternion;
    private Quaternion initialRotation;

    private int look;

    //private MultiAimContraint MultiAimConstraintVariable;

    void Start() {
        //set weight value for later as initial value
        // weight = GetComponent<MultiAimContraint>().weight;
        rotationSpeed = initialRotSpeed;
        moveSpeed = initialMoveSpeed;

        initialRotation = headBone.localRotation;
    }

    // Update is called once per frame
    void Update() {
        //check distance
        if (distance != Vector3.Distance(transform.position, player_transform.position)) {
            distance = Vector3.Distance(transform.position, player_transform.position);
        }
        //if the player is behind do not look at player
        //disable gameObject if too far from player
        if (distance <= maxDistance && player_transform.position.z > transform.position.z) {
            look = 1;
        }
        else {
            look = 0;
        }




        switch (look) {
            case 0:
                rotationQuaternion = Quaternion.Slerp(rotationQuaternion, initialRotation, rotationSpeed * Time.deltaTime);
                headBone.localRotation = rotationQuaternion;
                break;
            case 1:
                float tempY = player_transform.position.y;

                Vector3 targetPosition = new Vector3(player_transform.position.x, tempY + y_aim_correction, player_transform.position.z);

                rotationQuaternion = Quaternion.Slerp(rotationQuaternion, Quaternion.LookRotation(/*player_transform.localPosition*/targetPosition - transform.position), rotationSpeed * Time.deltaTime);
                rotationQuaternion.Set(rotationQuaternion.x,rotationQuaternion.y,transform.rotation.z,rotationQuaternion.w); // change the rotation on only the y and w
                //rotationQuaternion.z = transform.rotation.z;
                headBone.localRotation = rotationQuaternion;


                //headBone.localPosition += headBone.forward * moveSpeed * Time.deltaTime;
                break;
        }
    }
}
