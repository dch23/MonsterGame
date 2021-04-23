using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    Animator animator;

    enum stateTypes:int {idle = 0, walk = 1, run = 2};

    // stateTypes state = (int)stateTypes.idle;
    int state;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        state = (int)stateTypes.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                state = (int)stateTypes.run;
            }
            else {
                state = (int)stateTypes.walk;
            }
        }
        else {
            state = (int)stateTypes.idle;
        }
        animator.SetInteger("state", state);
    }
}
