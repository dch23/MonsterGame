using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class headAttackScript : MonoBehaviour
{
    // public Transform headBone;

    private Animator animator;

    private float distance;

    public GameObject head;
    private HealthScript healthScript;
    private FirstPersonController FirstPersonController;

    private int damageCount = 2;

    private int initialAttackCooldown = 100;
    private int attackCooldown;

    private float attackRadius = 0.7f;

    private bool attack;

    //freeze vaR

    public bool freezePlayer = false;
    public int stunMaxTime = 100;
    public int stunTime = 0;
    //
    private bool updatePosition;

    private float tempX;
    private float tempY;
    private float tempZ;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();

        attackCooldown = 0; // so the player gets attacked intantly when the player comes close but then has to wait for another attack
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(stunTime);
        //stun
        if (stunTime > 0) { // start the stun timer
            stunTime--;
        }
        //

        RaycastHit hit;



        //new forwards fo rhte 2 raycasts

        Vector3 attackRay2ForwardVector = new Vector3(head.transform.forward.x - 0.5f, head.transform.forward.y,head.transform.forward.z);
        Vector3 attackRay3ForwardVector = new Vector3(head.transform.forward.x + 0.5f, head.transform.forward.y,head.transform.forward.z);

        bool attackRay = Physics.Raycast(head.transform.position,head.transform.forward, out hit, attackRadius);
        bool attackRay2 = Physics.Raycast(head.transform.position,attackRay2ForwardVector, out hit, attackRadius);
        bool attackRay3 = Physics.Raycast(head.transform.position,attackRay3ForwardVector, out hit, attackRadius);
        Debug.DrawRay(head.transform.position, head.transform.forward);
        Debug.DrawRay(head.transform.position, attackRay2ForwardVector);
        Debug.DrawRay(head.transform.position, attackRay3ForwardVector);

        if ((attackRay2 || attackRay || attackRay3) && hit.transform != null) {
            if (hit.transform.gameObject.tag == "Player") {
                attack = true;
                healthScript = hit.transform.gameObject.GetComponent<HealthScript>();


                if (stunTime == 0) {
                    freezePlayer = true;
                }
                else {
                    freezePlayer = false;
                }
                if (freezePlayer == true) {
                    //freeze player
                    if (updatePosition) {
                        //freeze player
                        tempX = hit.transform.position.x;
                        tempY = hit.transform.position.y;
                        tempZ = hit.transform.position.z;
                        updatePosition = false;
                    }

                    Vector3 tempPosition = new Vector3(tempX,tempY,tempZ);

                    hit.transform.position = tempPosition;

                    // //get player vars and make it so the player cannot walk away
                    FirstPersonController = hit.transform.gameObject.GetComponent<FirstPersonController>();
                    FirstPersonController.m_WalkSpeed = 0f;
                    FirstPersonController.m_RunSpeed = 0f;
                }
                else {
                    //give the player back its speed
                    FirstPersonController = hit.transform.gameObject.GetComponent<FirstPersonController>();
                    FirstPersonController.m_WalkSpeed = FirstPersonController.m_InitialWalkSpeed;
                    FirstPersonController.m_RunSpeed = FirstPersonController.m_InitialRunSpeed;
                }
            }
        }
        else {
            attack = false;

            //unfreeze
            freezePlayer = false;
            updatePosition = true;


        }

        if (attack) {
            animator.SetBool("bite",true);
            if (attackCooldown <= 0) {


                healthScript.health -= damageCount;

                //play sound
                FindObjectOfType<audioManager>().Play("wallHeadBiteSound");

                attackCooldown = initialAttackCooldown;
            }
            else {
                attackCooldown--;
            }
        }
        else {
            animator.SetBool("bite",false);
            attackCooldown = 0;
        }

        // Debug.Log(animator.GetBool("bite"));

    }
}
