using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statueAttackScript : MonoBehaviour
{
    public HealthScript playerHealth;

    private Animator animator;
    private Rigidbody rb;

    public Transform player_transform;
    public Rigidbody player_rb;

    private float attackRadius = 5f;
    private float distance;

    private float punchStrength = 3f;
    private bool punch;
    private float punchDynamicForce;
    private float punchSpeed = 5f;


    private bool damage;
    private int damageCount = 30;

    private bool runTheAnimationTime; // when to decrease the punchAnimationTime var.

    private int initialPunchAnimationTime = 14; // this number is very sensitive to change;
    private int punchAnimationTime; // this number is a timer set with the initialPunchAnimationTime

    //fast rotate to align wiht player variables
    private float initialRotSpeed = 20f;
	private float rotationSpeed;
    private Quaternion rotationQuaternion;

    // private StatueFollowAi followScript;


    // Start is called before the first frame update
    void Start()
    {

        //set the timer
        punchAnimationTime = initialPunchAnimationTime; // set the timer

        punchDynamicForce = punchStrength; // set the decreasing force

        punch = false; // set punching to false;

        // fetch the components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();


        rotationSpeed = initialRotSpeed; // set the rotation speed to the initial rotation speed

        // player_transform = GameObject.FindGameObjectsWithTag("Player").gameObject.GetComponent<Transform>();

        // followScript = GetComponent<StatueFollowAi>();


    }

    // Update is called once per frame
    void Update()
    {


        // Debug.Log(animator.GetBool("attack"));

        if (punch) { // if im punching

            if (damage == false) {
                playerHealth.health -= damageCount;
                if (damage != true) {
                    damage = true;
                }
            }

            punchDynamicForce -= punchSpeed * Time.deltaTime; // start decreaseing the punchDynamicForce to slow down the acceleration from the force
            if (punchDynamicForce <= 0) { // if the force is 0 or negative the do the following
                punchDynamicForce = punchDynamicForce; // reset the force
                punch = false; // punch is false
            }
            else {
                player_transform.position += transform.forward * punchDynamicForce; // apply the force to the players transform
            }
            //Debug.Log(punchStrength);
        }
        else {
            if (damage != false) {
                damage = false;
            }
        }


        // Debug.Log("attack is " + animator.GetBool("attack"));
        // Debug.Log(distance);
        if (distance != Vector3.Distance(transform.position, player_transform.localPosition)) { // if the distance is not equal to the raw distance then make it equal to.
            distance = Vector3.Distance(transform.position, player_transform.localPosition);
        }
        if (distance < attackRadius) { // if the the player is in attack range

            if (animator.GetBool("attack") != true) { //
                animator.SetBool("attack", true); // set the attack animation variable to true;
            }

            runTheAnimationTime = true; // start the animation timer. at the end of this timer, the attack animation varible will be set to false

            // Attack();
            punchDynamicForce = punchStrength; // set the dynamic force

            //rotate to player
            rotationQuaternion = Quaternion.Slerp(rotationQuaternion, Quaternion.LookRotation(player_transform.position - transform.position), rotationSpeed * Time.deltaTime);
            rotationQuaternion.Set(transform.rotation.x,rotationQuaternion.y,transform.rotation.z,rotationQuaternion.w); // change the rotation on only the y and w
            transform.rotation = rotationQuaternion;

            punch = true; // set punch to true to start the forces on the player.

        }
        if (runTheAnimationTime == true) { // start the attack animation timer.
            punchAnimationTime--;
        }

        if (punchAnimationTime <= 0) { // if the timer ends then:
            runTheAnimationTime = false; // stop subtracting from the timer

            if (animator.GetBool("attack") != false) {
                    animator.SetBool("attack", false); // set the attack animation variable to false
            }

            punchAnimationTime = initialPunchAnimationTime; // set the timer back to its original value
        }

        //animation end
        // if(animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|run") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.19f) {
        //     if (animator.GetBool("attack") != false) {
        //         animator.SetBool("attack", false);
        //     }
        // }

        //

        // else {
        //     //set Animation
        //     if (animator.GetBool("attack") != false) {
        //         animator.SetBool("attack", false);
        //     }
        // }

    }

    void Attack() {
        //set Animation
        // animator.SetBool("attack", true);

        //rotate to player
        // rotationQuaternion = Quaternion.Slerp(rotationQuaternion, Quaternion.LookRotation(player_transform.position - transform.position), rotationSpeed * Time.deltaTime);
        // rotationQuaternion.Set(transform.rotation.x,rotationQuaternion.y,transform.rotation.z,rotationQuaternion.w); // change the rotation on only the y and w
        // transform.rotation = rotationQuaternion;

        //
        //punch player
        //direction to punch
        // Vector3 direction = player_transform.position - transform.position;
        //add force
        // player_rb.AddForce(transform.forward * punchStrength, ForceMode.Impulse);
        // rb.AddForce(transform.forward * punchStrength, ForceMode.Impulse);
        // rb.velocity = -transform.forward * punchStrength;
        // punch = true;

    }
}
