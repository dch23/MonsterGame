using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderAttackScript2 : MonoBehaviour
{
    // CapsuleCollider collider;
    // public CapsuleCollider playerCollider;

    //
    public Transform player_transform;
    //


    private float separateRayDistance = 0.2f;
    private float rayForwardStartPosition = 0.4f;


    public int damage;

    public HealthScript healthScript;

    private float attackRadius = 10f;

    private float attackRaySpread = 0.2f;

    private float initialAttackCooldown = 50;
    private float attackCooldown;

    private float cooldownVariation = 20;

    public float distance;

    // Start is called before the first frame update
    void Start() {
        //
        // //refrences
        // collider = GetComponent<CapsuleCollider>();
        //
        attackCooldown = initialAttackCooldown;    //set attack cooldown
    }

    void OnCollisionEnter(Collision playerCollision) {
        if (playerCollision.gameObject.tag == "Player") {
            if (attackCooldown < 1) {
                Attack();
            }

        }
    }

    // Update is called once per frame
    void Update() {

        if (distance != Vector3.Distance(transform.position, player_transform.position)) {
            distance = Vector3.Distance(transform.position, player_transform.position);
        }

        //cooldown
        if (attackCooldown > 0) {
            attackCooldown --;
        }
        else {
            attackCooldown = initialAttackCooldown;
            if (distance <= attackRadius) {
                Attack();
            }
        }

        //
        int temp = 0;


        //Debug.Log(temp);

        //touching
        // if (collider.IsTouching(playerCollider)) {
        //     if (attackCooldown < 1) {
        //         Attack();
        //     }
        // }
    }
    void Attack() { //attack
        //randomize
        attackCooldown = Random.Range(-cooldownVariation + initialAttackCooldown, cooldownVariation + initialAttackCooldown);
        //
        RaycastHit hit;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //
        // float temp_centreX = GetComponent<Renderer>().bounds.center.x; //get the centre of the renderer
        //
        // float temp_y = transform.position.y; // get y of the parent position
        // float temp_x = transform.position.x; //get x of parent position
        // float temp_z = transform.position.z; // get z of parent position
        //
        // Vector3 firstRayPosition = new Vector3(temp_centreX/*temp_x*/ + attackRaySpread, temp_y + 1f, temp_z + 1f); // create a start ray position dependent on the temp variables
        // Vector3 secondRayPosition = new Vector3(temp_centreX/*temp_x*/ - attackRaySpread, temp_y + 1f, temp_z + 1f);

        // Debug.DrawRay(firstRayPosition, forward); Debug.DrawRay(secondRayPosition, forward);

        //bool forwardRay = Physics.Raycast(transform.position,forward,out hit, attackRadius);
        // bool ray1 = Physics.Raycast(firstRayPosition,forward,out hit, attackRadius);
        // bool ray2 = Physics.Raycast(secondRayPosition,forward,out hit, attackRadius);
        // Vector3 newForward = new Vector3(transform.forward.x,transform.forward.y-0.03f,transform.forward.z);
        // Vector3 firstRayPosition = new Vector3(temp_x - separateRayDistance, temp_y, temp_z + rayForwardStartPosition);
        // Vector3 secondRayPosition = new Vector3(temp_x + separateRayDistance, temp_y, temp_z + rayForwardStartPosition);
        // Vector3 thirdRayPosition = new Vector3(temp_x, temp_y, temp_z);
        // bool ray1 = Physics.Raycast(/*firstRayPosition*/thirdRayPosition,forward,out hit, attackRadius);
        // bool ray2 = Physics.Raycast(secondRayPosition,transform.forward,out hit, attackRadius);
        // bool ray3 = Physics.Raycast(thirdRayPosition,-transform.up,out hit, attackRadius);
        // // Debug.DrawRay(firstRayPosition,transform.forward);
        // // Debug.DrawRay(secondRayPosition,transform.forward);
        // Debug.DrawRay(transform.position,forward);

        // if (ray1 || ray2 || ray3) {
        //     if (hit.transform == null) {
        //         return;
        //     }
        //     if (hit.transform.gameObject.tag == "Player") {
        //         healthScript.health -= damage;
        //         //Debug.Log(healthScript.health);
        //     }
        // }
        float temp_y = transform.position.y; // get y of the parent position
        float temp_x = transform.position.x; //get x of parent position
        float temp_z = transform.position.z; // get z of parent position

        bool forwardRay = Physics.Raycast(transform.position,forward,out hit, attackRadius);
        //Vector3 newForward = new Vector3(transform.forward.x,transform.forward.y-0.03f,transform.forward.z);
        Vector3 firstRayPosition = new Vector3(temp_x, temp_y, temp_z);
        Vector3 secondRayPosition = new Vector3(temp_x, temp_y, temp_z);
        bool ray1 = Physics.Raycast(firstRayPosition,transform.forward,out hit, attackRadius);
        bool ray2 = Physics.Raycast(secondRayPosition,transform.up,out hit, attackRadius);
        Debug.DrawRay(firstRayPosition,forward);
        Debug.DrawRay(secondRayPosition,transform.up);
        //Debug.DrawRay(firstRayPosition,forward);

        if (ray1 || ray2) {
            if (hit.transform == null) {
                return;
            }
            if (hit.transform.gameObject.tag == "Player") {
                healthScript.health -= damage;
                //Debug.Log(healthScript.health);
            }
        }
    }
}
