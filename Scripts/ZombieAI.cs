using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieAI : MonoBehaviour
{
    public GameObject fps;//the main camera of the player
    public Transform Player;//the player
     private Animator animator;//the variables that manages the animations
    private NavMeshAgent nma;//the NavMashAgent which is the AI component that will make the enemies go toward the players
    public float maxDist;//the maxim distace it can have by the player
    public float minDist;//the minim distance
    public float MoveSpeed ;//the speed of the AI
    private Collider[] ragdollColliders;//vector for the ragdoll coliders
    private Rigidbody[] ragdollRigidbodies;//vector for the ragdoll rigidbodies
    public float timer = 0.0f;
    public FirstPersonController firstPersonController;//the FirstPersonController
    //initializing the necesarry variables
    public void Start(){
         timer = Time.time;
        nma=GetComponent<NavMeshAgent>();
        ragdollColliders=GetComponentsInChildren<Collider>();
        ragdollRigidbodies=GetComponentsInChildren<Rigidbody>();
        animator=GetComponentInChildren<Animator>();
        firstPersonController=FindObjectOfType(typeof(FirstPersonController)) as FirstPersonController;
        //disabling the colliders 
        foreach(Collider col in ragdollColliders){
            if(!col.CompareTag("Zombie")){
            col.enabled=false;
        }
        }
        //disabling the rigidBodies
        foreach(Rigidbody rb in ragdollRigidbodies){
            rb.isKinematic=true;
        }
    }
    
    void Update()
    {
        //the function for making the enemy chase the player
        GoAfterPlayer();
    }
   

    public void GoAfterPlayer(){
        //if the distance between the player and the enemy is bigger than the minim distance the enemy will go toward the player
        if(Vector3.Distance(transform.position,Player.position) >= minDist){
          transform.position += transform.forward*MoveSpeed*Time.deltaTime;
          //we set the AI destination to the one of the player
          nma.destination=fps.transform.position;
     //if the distance between the player is less than the minim distance we make the enemy stop and stard attacking the player
    }else{
            
                nma.isStopped=true;//we stop the AI
                animator.SetBool("isNextToPlayer",true);//we trigger the attack animation
                //every 1 second we attack the player and decreas it's health
                if (Time.time - timer > 1) {
                if(animator.GetCurrentAnimatorStateInfo(0).IsTag("ZombieHit")){

                    firstPersonController.OnHit();
                    Debug.Log(playerHealth);
                    timer = Time.time;
                }
            }
            }   
    }
}
