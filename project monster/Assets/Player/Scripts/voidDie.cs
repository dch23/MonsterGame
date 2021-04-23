using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class voidDie : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10f) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
