using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class trapPlayer : MonoBehaviour
{
    [HideInInspector]
    public FirstPersonController playerScript; //get the player controller. this is public so the enemy health can access it so it can chagne the players speed back to its initial speed before dying

    private Animator animator;

    private float attackRadius = 3f;//attack radius;

    private RaycastHit hit;//the output of the raycast

    void Start() {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool ray1 = Physics.Raycast(transform.position, transform.up, out hit, attackRadius);//raycast that points up

        if (ray1) {
            Freeze();//freeze player;
        }

        // Debug.DrawRay(transform.position, transform.up);
    }
    void Freeze() {
        if (hit.transform != null) {//if the ray hit something
            if (hit.transform.gameObject.tag == "Player") {//if the ray hit the player
                playerScript = hit.transform.gameObject.GetComponent<FirstPersonController>();//fetch the player controller

                //animation
                animator.SetBool("attack", true);

                //change the players speed to 0 so the player cannot move
                playerScript.m_WalkSpeed = 0f;
                playerScript.m_RunSpeed = 0f;
            }
        }
    }
}
