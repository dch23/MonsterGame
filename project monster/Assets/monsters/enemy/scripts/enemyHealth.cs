using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float health;
    // Update is called once per frame
    void Update()
    {
        if (health <= 0f) {
            Die();
        }
    }
    void Die() {

        //if it is a trap then release the player first
        if (gameObject.tag == "trap") {//if the enemy is a trap
            trapPlayer trapPlayer = GetComponent<trapPlayer>();//fetch the trapping player script
            if (trapPlayer.playerScript != null){
                trapPlayer.playerScript.m_WalkSpeed = trapPlayer.playerScript.m_InitialWalkSpeed;//change the walkspeed back to it original value from teh trapPlayer script
                trapPlayer.playerScript.m_RunSpeed = trapPlayer.playerScript.m_InitialRunSpeed;//change the runspeed back to it original value from teh trapPlayer script
            }
        }
        Destroy(gameObject); //destroy this enemy; or object;
    }
}
