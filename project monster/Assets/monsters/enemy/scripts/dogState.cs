using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class dogState : MonoBehaviour
{
    private Animator animator;

    private float distance;

    private float followRadius = 24f;

    private NavMeshAgent agent;

    private Transform player_transform;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player_transform = GameObject.FindWithTag("Player").transform;

        animator = transform.GetChild(0).GetComponent<Animator>(); // this only works cause the first child of the wolf object is the model, mst change if order changes

    }

    // Update is called once per frame
    void Update()
    {
        if (distance != Vector3.Distance(transform.position, player_transform.position)) { //distance
            distance = Vector3.Distance(transform.position, player_transform.position);
        }

        if (distance <= followRadius) {
            agent.SetDestination(player_transform.position);

            animator.speed = Mathf.Clamp(Random.Range(animator.speed-0.2f,animator.speed+0.2f),1f,1.6f);
            animator.SetBool("Run", true);
        }
        else {
            agent.ResetPath();
            animator.speed = Mathf.Clamp(Random.Range(animator.speed-0.2f,animator.speed+0.2f),0.7f,1.3f);
            animator.SetBool("Run", false);
        }
    }
}
