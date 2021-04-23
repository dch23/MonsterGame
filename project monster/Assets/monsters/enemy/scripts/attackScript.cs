using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackScript : MonoBehaviour
{

    GameObject playerGO;
    Transform player_transform;

    float distance;

    //customizability
    public enum MonsterType:int {Dog = 0, Zombie = 1, Trap = 2, WallHead = 3, Spider = 4, Statue = 5};
    public MonsterType monsterType = (int)MonsterType.Dog;
    //


    public int damage;

    private HealthScript healthScript;

    private float attackRadius = 4f;

    private float dogAttackRaySpread = 0.2f;

    public float initialAttackCooldown = 40;
    private float attackCooldown;

    private float cooldownVariation = 3;

    RaycastHit hit;


    //exclusively for nav mesh agents
    private float initialRotSpeed = 20f;
	private float rotationSpeed;

    // Start is called before the first frame update
    void Start() {
        attackCooldown = initialAttackCooldown;    //set attack cooldown

    }
    //isGrounded
    void OnCollisionEnter(Collision playerCollision) {
        if (playerCollision.gameObject.tag == "Player") {
            if (attackCooldown < 1) {
                Attack();
            }

        }
    }

    public GameObject FindClosestPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // Update is called once per frame
    void Update() {


        // if (distance != Vector3.Distance(transform.position, player_transform.localPosition)) {
        //     distance = Vector3.Distance(transform.position, player_transform.localPosition);
        // }

        // if (distance < 7f) {
        //     // //cooldown
        //     // if (attackCooldown > 0) {
        //     //     attackCooldown --;
        //     // }
        //     // else {
        //     //     attackCooldown = initialAttackCooldown;
        //     //     Attack();
        //     // }
        // }
        //cooldown
        if (attackCooldown > 0) {
            attackCooldown --;
        }
        else {
            attackCooldown = initialAttackCooldown;
            Attack();
        }
    }
    void Attack() { //attack

        switch((int) monsterType) {
            case 0: //dog
                //randomize
                attackCooldown = Random.Range(-cooldownVariation + initialAttackCooldown, cooldownVariation + initialAttackCooldown);
                //
                // RaycastHit hit;



                Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

                // float temp_centreX = GetComponent<Renderer>().bounds.center.x; //get the centre of the renderer

                // float temp_y = transform.position.y; // get y of the parent position
                // float temp_x = transform.position.x - 0.3f; //get x of parent position
                // float temp_z = transform.position.z; // get z of parent position

                Vector3 firstRayPosition = new Vector3(transform.position.x + dogAttackRaySpread, transform.position.y+1f, transform.position.z); // create a start ray position dependent on the temp variables
                Vector3 secondRayPosition = new Vector3(transform.position.x - dogAttackRaySpread, transform.position.y+1f, transform.position.z);


                Debug.DrawRay(firstRayPosition, forward); Debug.DrawRay(secondRayPosition, forward);

                //bool forwardRay = Physics.Raycast(transform.position,forward,out hit, attackRadius);
                bool ray1 = Physics.Raycast(firstRayPosition,forward,out hit, attackRadius);
                bool ray2 = Physics.Raycast(secondRayPosition,forward,out hit, attackRadius);

                if (/* forwardRay ||*/ ray1 || ray2) {
                    if (hit.transform == null) {
                        return;
                    }
                    if (hit.transform.gameObject.tag == "Player") {

                        healthScript = hit.transform.gameObject.GetComponent<HealthScript>();

                        healthScript.health -= damage;

                        //make sound
                        FindObjectOfType<audioManager>().Play("dogBiteSound");

                        //Debug.Log(healthScript.health);
                    }
                }
                Debug.DrawRay(transform.position, transform.forward, Color.red);

                break;
            case 1: //Zombie
                break;
            case 2: //trap
                bool upRay = Physics.Raycast(transform.position, transform.up, out hit, attackRadius);//raycast that points up
                if (upRay) {

                    if (hit.transform != null) {//if the ray hit something
                        if (hit.transform.gameObject.tag == "Player") {//if the ray hit the player
                            HealthScript healthScript = hit.transform.gameObject.GetComponent<HealthScript>();

                            healthScript.health -= damage;

                            //make sound
                            FindObjectOfType<audioManager>().Play("dogBiteSound");
                        }
                    }
                }
                Debug.DrawRay(transform.position,transform.up);

                break;
            case 3:

                break;
            case 4://spider
                //randomize
                attackCooldown = Random.Range(-cooldownVariation + initialAttackCooldown, cooldownVariation + initialAttackCooldown);
                //

                Vector3 forwardSpider = transform.TransformDirection(Vector3.forward) * 10;

                Vector3 firstRayPositionSpider = new Vector3(transform.position.x + dogAttackRaySpread, transform.position.y+1f, transform.position.z); // create a start ray position dependent on the temp variables
                Vector3 secondRayPositionSpider = new Vector3(transform.position.x - dogAttackRaySpread, transform.position.y+1f, transform.position.z);


                // Debug.DrawRay(firstRayPositionSpider, forwardSpider); Debug.DrawRay(secondRayPositionSpider, forwardSpider);

                bool ray1Spider = Physics.Raycast(firstRayPositionSpider,forwardSpider,out hit, attackRadius);
                bool ray2Spider = Physics.Raycast(secondRayPositionSpider,forwardSpider,out hit, attackRadius);

                if (/* forwardRay ||*/ ray1Spider || ray2Spider) {
                    if (hit.transform == null) {
                        return;
                    }
                    if (hit.transform.gameObject.tag == "Player") {

                        healthScript = hit.transform.gameObject.GetComponent<HealthScript>();

                        healthScript.health -= damage;

                        //make sound
                        FindObjectOfType<audioManager>().Play("dogBiteSound");
                    }
                }
                // Debug.DrawRay(transform.position, transform.forward, Color.red);
                break;
            case 5:
                playerGO = FindClosestPlayer();
                player_transform = playerGO.transform;

                if (distance != Vector3.Distance(transform.position, player_transform.localPosition)) {
                    distance = Vector3.Distance(transform.position, player_transform.localPosition);
                }
                

                break;
        }



    }
}
