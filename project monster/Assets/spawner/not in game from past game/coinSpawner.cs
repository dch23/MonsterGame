using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSpawner : MonoBehaviour
{
    //private GameObject[] getCount;
    // public GameObject[] coinlist;
    private int maxCoinCount = 5;
    private int coinCount;

    public GameObject coinObject;
    private int intialCooldown = 120;
    private int cooldown;

    //private var gos : GameObject[];

    // Start is called before the first frame update
    void Start()
    {
        cooldown = intialCooldown;
    }

    // Update is called once per frame
    void Update(){
        if(cooldown > maxCoinCount){
            cooldown--;
        }
        else{
            cooldown = intialCooldown;
        }

        //cooldown
        if (cooldown == 0) {
            float randomX = Random.Range(-5f, 5f);
            float randomZ = Random.Range(-5f, 5f);

            Vector3 randomLocation = new Vector3(randomX, 2f, randomZ);

            Quaternion rotation = new Quaternion(0,0,0,0);

            // GameObject tempGO = coinObject.FindGameObjectWithTag("coin");
            // coinCount = tempGO.Length;

            // getCount = GameObject.FindGameObjectWithTag("coin");
            // coinCount = getCount.Length;

            //coinCount = GameObject.FindGameObjectWithTag("coin").Length;

            // if(coinCount <= maxCoinCount){
            //     GameObject coin = Instantiate(coinObject,randomLocation, rotation);
            // }

            // if(GameObject.FindGameObjectWithTag("coin").Length < maxCoinCount){
            //     GameObject coin = Instantiate(coinObject,randomLocation, rotation);
            // }
          // coinlist = GameObject.FindGameObjectWithTag("coin");
          // coinCount = coinlist.Length;

        }

    }
}
