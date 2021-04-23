using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderAttack : MonoBehaviour
{

    //variables
    private int attackCooldown = 30;

    void Start() {
        //get the state vaiabrle and if state is following then check cooldown
    }

    void OnCollisionEnter(Collision playerCollision) {
        if (playerCollision.gameObject.tag == "Player") {
            if (attackCooldown <= 0) {
                Attack();
            }
        }
    }

    void Attack() {

    }
}
