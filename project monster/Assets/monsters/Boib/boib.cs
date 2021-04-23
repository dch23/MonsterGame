using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boib : MonoBehaviour
{
    Transform player_transform;
    NavMeshAgent agent;
    Animator animator;

    float distance;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        player_transform = GameObject.FindWithTag("Player").transform;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        distance = Vector3.Distance(transform.position, player_transform.position);

        if (distance > 3f) {
            agent.SetDestination(player_transform.position);
            animator.SetBool("Run",true);
        }
        else {
            agent.ResetPath();
            animator.SetBool("Run",false);
        }
    }
}
