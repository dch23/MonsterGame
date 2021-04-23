using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Gun : MonoBehaviour {

    //attacked!
    private headAttackScript wallHeadAttackScript;
    //


    [Header("Fetch Scripts/Gameobjects:")]
    //fetch other scripts and GameObjects
    public cameraRecoil cameraRecoil;//camrecoil access
    public GameObject crosshair;//crosshair
    public FirstPersonController playerScript;//to player script refrenc


    [Header("Properties:")]
    //properties
    public bool auto = false;
    public enum GunList {Pistol, Shotgun, AssaultRifle, SMG, Sniper};
    public GunList GunType = GunList.Pistol;
    public int bulletNumber = 1;//number of bullets
    public float damage = 10f;
    public float range = 100f;

    [Header("Accuracy:")]
    //accuracy
    public float initialBulletSpread = 0;
    private float bulletSpread;
    private float bulletSpreadDivider = 50;
    public float bulletSpreadAimDivider = 10;

    [Header("Ammo:")]
    //ammo
    public Transform ammoCountText;//ammo count
    public float reloadTime = 10f;
    public int maxAmmo = 10;
    private int currentAmmo;

    [Header("Reloading:")]
    //reloading
    private bool isReloading = false;

    [Header("Fire Rate:")]
    //fire rate
    public float fireRate = 15f;
    private float next_time_to_fire = 0f;

    [Header("Sounds:")]
    //sound
    public AudioSource pistolShot;
    public AudioSource ARShot;
    public AudioSource shotgunShot;
    public AudioSource smgShot;
    public AudioSource sniperShot;

    [Header("Muzzle Flash:")]
    public ParticleSystem muzzleFlash;//muzzle flash

    [Header("Player Camera:")]
    public Camera fpsCam;//camera

    [Header("Impact Effect:")]
    public GameObject impactEffect;//gunSpark
    public float impactForce = 20f;//impact force

    [Header("Refrence Points:")]
    public Transform recoilPosition;
    public Transform rotationPoint;

    [Header("Speed Settings:")]
    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;
    [Space(10)]
    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 18f;

    [Header("Amount Settings:")]
    public Vector3 RecoilRotation = new Vector3(10,5,7);
    public Vector3 RecoilKickBack = new Vector3(0.015f,0f,-0.2f);
    [Space(10)]
    public Vector3 RecoilRotationAim = new Vector3(10,4,6);
    public Vector3 RecoilKickBackAim = new Vector3(0.015f,0f,-0.2f);

    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 Rot;

    [Header("Aiming: ")]
    //aiming
    public bool aiming;
    public Vector3 AimVector = new Vector3(0f,0f,0f);
    public float aimspeed = 10f;
    private Vector3 noAimVector;

    void FixedUpdate () {
        /*
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        Rot = Vector3.Slerp(Rot, currentRotation, rotationSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(Rot);
        //playerScript.m_Camera.transform.localRotation = Quaternion.Euler(Rot);
        */
        //
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
        Rot = Vector3.Slerp(Rot, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(Rot);
    }

    //
    void Awake () {
        //no aim vector is set here so it can be called before onenable
        noAimVector = transform.localPosition;
    }

    //start is called at the start of the game
    void Start() {

    //    noAimVector = transform.localPosition; // this was disabled because i moved it to void awake because this must happen before the onenable.transform local poisiotn to no aimvector
        //
        currentAmmo = maxAmmo;

        //start variables
        bulletSpread = initialBulletSpread;
    }

    //when i activate my gun, then set reloading to false
    void OnEnable () {
        isReloading = false;
        //change the transfomrm
        transform.localPosition = noAimVector;
    }

	// Update is called once per frame
	void Update () {
        //update noaim Vector3Vector3
        //NoAimVector = transform.localPosition;
        //aiming
        if (Input.GetMouseButton(1)) {

            //gun recoil Aiming
            aiming = true;
            //

            //cam Recoil
            cameraRecoil.aiming = true;
            //

            transform.localPosition = Vector3.Slerp(transform.localPosition, AimVector, aimspeed * Time.deltaTime);
            //deactivate the crosshair
            crosshair.SetActive(false);
            //better accuracy when Aiming with pistol and AssaultRifle
            if ((GunType == GunList.Pistol) || (GunType == GunList.AssaultRifle)) {
                bulletSpread = initialBulletSpread/bulletSpreadAimDivider;
            }


        }
        else {

            //gunrecoil
            aiming = false;
            //

            //cameraRecoil
            cameraRecoil.aiming = false;
            //
            transform.localPosition = Vector3.Slerp(transform.localPosition, noAimVector, aimspeed * Time.deltaTime);
            crosshair.SetActive(true);
            //set bullet spread to it's original value
            bulletSpread = initialBulletSpread;
        }
        //reloading
        if (isReloading != true) {
            //ammo
            if ((currentAmmo <= 0) || ((Input.GetKeyDown(KeyCode.R)) && (currentAmmo < maxAmmo))) {
                StartCoroutine(Reload());
                return;
            }
            //fire button and fire rate
            if (auto != true) { //if it is not auto then leave it for pressing
                if (Input.GetMouseButtonDown(0) && Time.time >= next_time_to_fire) {

                    next_time_to_fire = Time.time + 1f / fireRate;
                    Shoot();
                }
            }
            else { //if it is auto then make it so you can hold then fire button
                if (Input.GetMouseButton(0) && Time.time >= next_time_to_fire) {
                    next_time_to_fire = Time.time + 1f / fireRate;
                    Shoot();
                }
            }
        }

        //show the ammo count
        ammoCountText.GetComponent<Text>().text = "Ammo " + currentAmmo.ToString() + " / " + maxAmmo.ToString();
	}

    IEnumerator Reload () {
        isReloading = true;

        Debug.Log("Reloading . . .");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        Debug.Log("Finished Reloading");
        isReloading = false;
    }

    void Shoot () {

        //gun recoil
        if (aiming) {
            rotationalRecoil += new Vector3(-RecoilRotationAim.x, Random.Range(-RecoilRotationAim.y, RecoilRotationAim.y), Random.Range(-RecoilRotationAim.z, RecoilRotationAim.z));
            positionalRecoil += new Vector3(Random.Range(-RecoilKickBackAim.x, RecoilKickBackAim.x), Random.Range(-RecoilKickBackAim.y, RecoilKickBackAim.y), RecoilKickBackAim.z);
        }
        else {
            rotationalRecoil += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
            positionalRecoil += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), Random.Range(-RecoilKickBack.y, RecoilKickBack.y), RecoilKickBack.z);
        }
        //

        //cam Recoil
        cameraRecoil.Fire();
        //
        //currentRotation += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z,RecoilRotation.z));

        //gun type
        if (GunType == GunList.Pistol) {
            pistolShot.Play(); // play the pistol shot sound
        }
        else if (GunType == GunList.Shotgun) {
            shotgunShot.Play(); // play the pistol shot sound
        }
        else if (GunType == GunList.AssaultRifle) {
            ARShot.Play(); // play the pistol shot sound
        }
        else if (GunType == GunList.SMG) {
            smgShot.Play(); // play the pistol shot sound
        }
        else if (GunType == GunList.Sniper) {
            sniperShot.Play();
        }

        //muzzle flash
        muzzleFlash.Play(); // play the muzzle flash

        int i = bulletNumber;

        while(i > 0) {
            RaycastHit hit;

            //get camera position but in front allitle so the ray cast does not start inside the player/
            Vector3 newCameraPosition = new Vector3(fpsCam.transform.localPosition.x,fpsCam.transform.localPosition.y,fpsCam.transform.localPosition.z);

            //layer masks
            int playerLayerMask = 1 << 9;    //player layer mask //this would not include layers exept 9
            int spawnerLayerMask = 1 << 14;
            int rampLayerMask = 1 << 11;

            playerLayerMask = ~playerLayerMask;    //so i want to invert it
            spawnerLayerMask = ~spawnerLayerMask;
            rampLayerMask = ~rampLayerMask;

            int layerMask = playerLayerMask + spawnerLayerMask + rampLayerMask;

            //this shoots out a raycast added to the accuracy toll
            if (Physics.Raycast(fpsCam.transform.position , fpsCam.transform.forward + new Vector3(Random.Range(-bulletSpread / bulletSpreadDivider,bulletSpread / bulletSpreadDivider),
            Random.Range(-bulletSpread,bulletSpread) / bulletSpreadDivider,Random.Range(-bulletSpread,bulletSpread) / bulletSpreadDivider), out hit, range, layerMask)) { // thi slayer mask maske sit so the player is not included
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();

                if (target != null) {
                    target.TakeDamage(damage);
                }

                //free player by shotin wallhead if frozen

                if (hit.transform.gameObject != null) {
                    if (hit.transform.gameObject.tag == "wallHead") {
                        wallHeadAttackScript = hit.transform.gameObject.GetComponent<headAttackScript>(); //fetch wallhead attack script

                        if (wallHeadAttackScript.freezePlayer == true) {
                            wallHeadAttackScript.stunTime = wallHeadAttackScript.stunMaxTime;
                        }
                    }
                }

                //damage enemies
                if (hit.transform.gameObject != null) { // if the player shot something
                    if (hit.transform.gameObject.GetComponent<enemyHealth>() != null) { // and it has an enemy health
                        enemyHealth enemyHealth = hit.transform.gameObject.GetComponent<enemyHealth>();

                        enemyHealth.health -= damage;    //decrease health
                    }
                }
            }

            //force when hit object
            if (hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //instantiate the impact effect
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);

            //ammo reduction
            currentAmmo--;

            //subtract to end the loop
            i--;
        }
    }
}
