using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class attackPlayer : MonoBehaviour
{
    private FirstPersonController playerScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter(Collision collidePlayer) {
        if (collidePlayer.gameObject.tag == "Player") {
            playerScript = collidePlayer.gameObject.GetComponent<FirstPersonController>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (playerScript != null) {
            playerScript.m_WalkSpeed = 0f;
            playerScript.m_RunSpeed = 0f;
        }
    }
}
