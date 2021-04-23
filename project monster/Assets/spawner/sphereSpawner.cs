using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereSpawner : MonoBehaviour
{
    public GameObject instance;
    public Renderer floor;
    public float spawnerThickness;

    private GameObject[] instances;
    private List<GameObject> ObjectsInRange = new List<GameObject>();

    float instanceHealth;

    int initialCooldown = 300;
    int cooldown;

    public int maxInstanceCount;
    int instanceCount;

    //location
    float randomX;
    float randomZ;
    public float raise;
    public float narrowFactor;


    // Start is called before the first frame update
    void Start() {
        cooldown = initialCooldown;//set cooldown

        //teleport the spawner to its floor
        transform.position = floor.transform.position;

        //make it a trigger collision and not physical
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        //change box collider to the mesh size of the spawner's floor
        float colliderSizeX = floor.bounds.size.x;
        float colliderSizeY = spawnerThickness;
        float colliderSizeZ = floor.bounds.size.z;

        Vector3 floorSize = new Vector3(colliderSizeX,colliderSizeY,colliderSizeZ);

        boxCollider.size = floorSize; // change size of box collider to the mesh size of the floor
    }

    public void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "zombie") {
            ObjectsInRange.Add(col.gameObject);
        }
     }
    public void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "zombie") {
            //Probably you'll have to calculate wich object it is
            ObjectsInRange.Remove(col.gameObject);
        }
     }

     void removeNullFromObjectsInRange () {
         //this loop moves the null element to the end of the list so the list does not have to move every other element back one
         for(int i = ObjectsInRange.Count - 1; i >= 0; i--) { // loop -1 because count is already plus one.
             if (ObjectsInRange[i] == null) {//if an element is null then
                 ObjectsInRange[i] = ObjectsInRange[ObjectsInRange.Count - 1];//move the null element to the end of the list
                 ObjectsInRange.RemoveAt(ObjectsInRange.Count - 1);//remove the element at the end of the list
             }
         }
     }

    // Update is called once per frame
    void Update() {
        removeNullFromObjectsInRange();

        //
        instanceCount = (int)ObjectsInRange.Count;  //count the list count(length) of instances

        if (cooldown == 0 && instanceCount < maxInstanceCount) { // if cooldown is finished and there are less than the max instance count then
            Spawn();    //spawn instance
        }
        else {
            if (instanceCount != maxInstanceCount) {    //if the spawner can still spawn instances then
                cooldown--; //decrease the cooldown
            }
        }
    }
    void Spawn() {
        float randomRotY = Random.Range(0f,360f);
        //random rotation calculation
        Quaternion randomRot = new Quaternion(instance.transform.rotation.x, randomRotY, instance.transform.rotation.z, instance.transform.rotation.w);

        //location calculation
        randomX = Random.Range(floor.bounds.min.x + narrowFactor,floor.bounds.max.x - narrowFactor); //random x
        randomZ = Random.Range(floor.bounds.min.z + narrowFactor,floor.bounds.max.z - narrowFactor); //random z

        Vector3 location = new Vector3(randomX, floor.bounds.max.y + raise, randomZ); //new location with random values;

        Instantiate(instance,location,transform.rotation);    //spawn the instance
        cooldown = initialCooldown; //reset the cooldown
    }
}
