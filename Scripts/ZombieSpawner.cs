using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ZombieSpawner : MonoBehaviour
{
    public GameObject Zombie;//the object for the enemy
    public Vector3 center;//the center of a 3D vector
    public Vector3 size;//the size of the 3D vector
    public float timer = 0.0f;//the timer for spawning the zombies
    public Image im; 
    // Start is called before the first frame update
    void Start()
    {
      timer = Time.time;
      im.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {   //we get the current scene
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;
        //if we are in the game scene we start spawning enemies every 5 seconds
        if (sceneName == "Game") 
         {im.enabled=true;
            if (Time.time - timer > 5) {
                Spawn();
                timer = Time.time;
    }   
         }

    }
    //the function that is managing the spawning
    public void Spawn(){
        // the position of where the spawner is located
       Vector3 pos =center+ new Vector3(Random.Range(-size.x/2,size.x/2),Random.Range(-size.y/2,size.y/2),Random.Range(-size.z/2,size.z/2));
       //we instantiate the zombie object
        Instantiate(Zombie,pos,Quaternion.identity);
        

    }
   //we draw the spawner and color it so we can see it 
    void OnDrawGizmosSelected(){
        Gizmos.color=new Color(1,0,0,0.5f);
        Gizmos.DrawCube(transform.localPosition+center,size);
    }
}
