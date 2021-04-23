using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public Transform HealthUI;    //get the transform of the health ui

    private bool isPlayer;

    private int defaultMaxHealth = 100;

    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        //if maxHealth is not set in the inspector, then set it
        if (maxHealth == 0) {
            maxHealth = defaultMaxHealth;
        }

        //when this script is run, the health will be at its max
        health = maxHealth;
        //check if this script is attached to a Player tag
        if (gameObject.tag == "Player") {
            isPlayer = true;
        }
        else {
            isPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer == false) {
            return;
        }
        if (health > -1) {
            HealthUI.GetComponent<Text>().text = "Health " + health.ToString() + " / " + maxHealth.ToString();
        }
        else {
            HealthUI.GetComponent<Text>().text = "Health 0 / " + maxHealth.ToString();
        }

        int temp_prev_health = health;

        if (health != temp_prev_health) {
            Debug.Log(health);
        }
        //restart scene
        if (health <= 0) {
            restart();
        }
    }

    void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
