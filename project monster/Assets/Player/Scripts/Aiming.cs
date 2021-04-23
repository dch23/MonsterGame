using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {

	public Vector3 NoAimVector;
	public Vector3 AimVector;

 	public float aimspeed;


    // Update is called once per frame
    void Update() {
		if (Input.GetMouseButton(1)) {
      		transform.localPosition = Vector3.Slerp(transform.localPosition, NoAimVector, aimspeed * Time.deltaTime);
		}
		else {
	      transform.localPosition = Vector3.Slerp(transform.localPosition, AimVector, aimspeed * Time.deltaTime);
	  	}
	}
}
