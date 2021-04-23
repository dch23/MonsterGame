using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderState : MonoBehaviour
{
    //script refrences
    Spider spider;
    spiderFollowAI followScript;
    SpiderNPCController idleScript;
    //
    Transform player_transform;
    private float distance;
    private int state;

    private float followRadius = 20f;
    private float attackRadius = 2f;

    private int Idle = 0;
    private int Follow = 1;
    private int Attack = 2;

    // Start is called before the first frame update

    void Start()
    {
        //get player transform
        player_transform = GameObject.FindWithTag("Player").transform;

        //access scripts
        followScript = GetComponent<spiderFollowAI>();
        idleScript = GetComponent<SpiderNPCController>();
        //
        state = Idle;

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player_transform.position);

        //check if player is close enough for the spider to switch to follow
        if (distance <= followRadius && distance > attackRadius) {
            state = Follow;
        }
        if (distance > followRadius) {
            state = Idle;
        }

        if (distance <= attackRadius) {
            state = Attack;
        }



        //switch between States
        switch (state) {
            case 0:
                followScript.enabled = false;
                idleScript.enabled = true;
                break;
            case 1:
                idleScript.enabled = false;
                followScript.enabled = true;
                break;
            case 2:
                followScript.enabled = false;
                idleScript.enabled = false;
                break;
        }
    }
}
