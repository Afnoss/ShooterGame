using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{   private Animator animator;//the variables that manages the animations
    private NavMeshAgent nma;//the NavMashAgent which is the AI component that will make the enemies go toward the players
    public float health =50f;//the health of the target
    private Collider[] ragdollColliders;//vector for the ragdoll coliders
    private Rigidbody[] ragdollRigidbodies;//vector for the ragdoll rigidbodies
    
    private int score=0;//initializing the score
    //initializing the variables necesary for the enemy, the animator,NavMeshAgent,the colliders and the rigid bodies
    public void Start(){
        animator=GetComponentInChildren<Animator>();
        nma=GetComponent<NavMeshAgent>();
        ragdollColliders=GetComponentsInChildren<Collider>();
         ragdollRigidbodies=GetComponentsInChildren<Rigidbody>();
         
    }
    //function that decreases the health of the target
    public void TakeDamage(float amount){
        health-=amount;
        //if the health is below or equal to 0 we call the function Die()
        if(health<=0f){
            Die();
        }
    }
    void Die(){
        //setting the speed of the agent to 0 so it does not move
        nma.speed=0;
        //we stop the animator
        animator.enabled=false;
        //enabling the colliders of the enemy
        foreach(Collider col in ragdollColliders){
            col.enabled=true;
        }
        //enabling the rigid bodies of the enemies
        foreach(Rigidbody rb in ragdollRigidbodies){
            if(!rb.CompareTag("Zombie")){
            rb.isKinematic=false;
        }
        }
        nma.isStopped=true;
        ++score;//we increase the score
        PlayerPrefs.SetInt("Score",score);//we save the score so it is not deleted after closing the game
        //we wait for 20 second before destroying the object so it does not occupy memory
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }
}
