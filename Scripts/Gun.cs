using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
       public int leftFingerId {get;set;} //variable for tracking left finger
    public int rightFingerId{get;set;}//variable for tracking right finger
    float halfScreenWidth; //variable for dividing the screen in 2
    public Transform cameraTransform;//variable for determinig the position, rotation, and scale of the camera 
    public float cameraSensivity;//variable for setting the sensitivity
    Vector2 lookInput;//variable for storing the location relative to the scene in world space 
    public float damage =10f;//variable for the damage given
    public float range=100f;//variable for the distance of the bullets
    public Camera fpsCam;//the camera used
    public ParticleSystem muzzleFlash;//initializing the variable for the muzzle flash of the gun
    public float impactForce=30f;//the impact force of the bullets upon hittin the obstacles
    public float fireRate=15f;//the speed of which the gun shoots
    private float nextTimeToFire=0f;//used for the shooting speed, how much we should wait before shooting the next bullet
    public GameObject impactEffect;//the effect when the bullets touches the target
    float cameraPitch; //vertical rotation of camera
    public int maxAmmo=100;//the max ammo
    private int currentAmmo;//variable for checking the available ammo
    public float reloadTime=1f;//the reload time
    private bool isReloading=false;//variable for checking if we are reloading or not
    ////initializing the id of the left and right finger id which is used for checking what part of the screen is touched
    void Start()
    {
        leftFingerId=-1;
        rightFingerId=-1;
        halfScreenWidth=Screen.width/2;
        //at the start of the game the current ammo equals to the max ammo
        currentAmmo=maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        //if it's reloading we terminate the execution of the method
          if(isReloading){
            return;
        }
        //if the ammo is lesser or equal to 0 we start the coroutine to reload and then terminate the execution of the method
        if(currentAmmo<=0){
            StartCoroutine(Reload());
            return;
        }

        GetTouchInput();
         if(leftFingerId!=-1&& Time.time>= nextTimeToFire){
            //we increment the time by 1 and divide it with the fireRate so we get the shooting frequency
            nextTimeToFire=Time.time+1f/fireRate;
            Debug.Log(currentAmmo);
            //function for shooting
            shoot();
        }
        
    }

    void OnEnable(){
        isReloading=false;
    }
    IEnumerator Reload(){
        isReloading=true;
        Debug.Log("Reloading");
        //we wait for 10 seconds then we set the currentAmmo a
        yield return new WaitForSeconds(10);
        currentAmmo=maxAmmo;
        isReloading=false;
        

    }
    public void GetTouchInput(){
        for(int i=0;i<Input.touchCount;++i){
                    Touch t=Input.GetTouch(i);
                    switch(t.phase){
                        case TouchPhase.Began:
                            if(t.position.x<halfScreenWidth&&leftFingerId==-1&& Time.time>= nextTimeToFire)
                            {
                                
                                leftFingerId=t.fingerId;
                                Debug.Log("Tracing left finger");
                            }
                            break;
                        case TouchPhase.Ended:
                        if(t.fingerId==leftFingerId){
                                leftFingerId=-1;
                                Debug.Log("Stopped tracking left finger");
                                
                            }
                            break;
                        case TouchPhase.Canceled:
                            if(t.fingerId==leftFingerId){
                                leftFingerId=-1;
                                Debug.Log("Stopped tracking left finger");
                                
                            }
                            break;

                    }
                }
    }
    //function for shooting
     void shoot(){
        muzzleFlash.Play();//we play the muzzleFlash particle effect
        RaycastHit hit;
        //we send a raycast from the center of the screen forward
        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit,range)){
            Debug.Log(hit.transform.name);
            //we get the hit object
            Target target=hit.transform.GetComponent<Target>();
            //if the target exist we deacrease it's healt
            if(target!=null){
                target.TakeDamage(damage);
            }
            //if the target has a rigid body we add force
            if(hit.rigidbody!=null){
                hit.rigidbody.AddForce(-hit.normal*impactForce);
            }
            //we instantiate the impact effect and then we destroy it
            GameObject impact =Instantiate(impactEffect,hit.point,Quaternion.LookRotation(hit.normal));
            Destroy(impact,1f);
        }

    }

}
