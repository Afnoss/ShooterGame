using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstPersonController : MonoBehaviour
{
    public int leftFingerId {get;set;} //variable for tracking left finger
    public int rightFingerId{get;set;}//variable for tracking right finger
    float halfScreenWidth; //variable for dividing the screen in 2
    public Transform cameraTransform;//variable for determinig the position, rotation, and scale of the camera 
    public float cameraSensivity;//variable for setting the sensitivity
    Vector2 lookInput;//variable for storing the location relative to the scene in world space 
    int playerHealth=100;//the health of the player
    float cameraPitch; //vertical rotation of camera
    //initializing the id of the left and right finger id which is used for checking what part of the screen is touched
    void Start()
    {
        leftFingerId=-1;
        rightFingerId=-1;
        halfScreenWidth=Screen.width/2;
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTouchInput();
        //if we touch the right side of the screen we can look around
        if(rightFingerId!=-1){
            LookAround();
        } 
    }
    //function for decreasing the player's health when hit
    public void OnHit(){
        //if the health is bigger then 0 we decrease the health by 1
        if(playerHealth>0){
        --playerHealth;
        Debug.Log(playerHealth);
    }//if the health is 0 or less the game ends and we load the menu
        else if(playerHealth<=0){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        }
    }
    //should have made a separate class for this function so the code does not repeat
    //function used for tracking the finger and what part of the screen it touches
    public void GetTouchInput(){
        for(int i=0;i<Input.touchCount;++i){
                    Touch t=Input.GetTouch(i);
                    switch(t.phase){
                        //in case we just touched the screen
                        case TouchPhase.Began:
                        //if we touch the left side of the screen we give leftFingerId the id of the touch
                            if(t.position.x<halfScreenWidth&&leftFingerId==-1)
                            {
                                leftFingerId=t.fingerId;
                                Debug.Log("Tracing left finger");
                            }//if we touch the right side of the screen we give leftFingerId the id of the touch
                            else if(t.position.x>halfScreenWidth&&rightFingerId==-1){
                                rightFingerId=t.fingerId;
                                Debug.Log("Tracing right finger");
                            }
                            break;
                        case TouchPhase.Ended:
                        //in case we stop touching the screen we check which part of the screen we stoped touching and give the variable -1 so the function LookAround won't be called
                        case TouchPhase.Canceled:
                            if(t.fingerId==leftFingerId){
                                leftFingerId=-1;
                                Debug.Log("Stopped tracking left finger");
                            }
                            else if(t.fingerId==rightFingerId){
                                rightFingerId=-1;
                                Debug.Log("Stopped tracking right finger");
                            }
                            break;
                        //if we move the finger while touching the screen we give the variable of where the finger is placed    
                        case TouchPhase.Moved:
                            if(t.fingerId==rightFingerId){
                                lookInput=t.deltaPosition*cameraSensivity*Time.deltaTime;
                            }
                            break;
                        //if we don't move the finger the vector is equal to 0
                        case TouchPhase.Stationary:
                            if(t.fingerId==rightFingerId){
                                lookInput=Vector2.zero;
                            }
                            break;

                    }
                }
    }//GetTouchInput end
    //the function for looking around
    void LookAround(){
        //we clamp the value given to camera pitch to be between -90 and 90
        cameraPitch=Mathf.Clamp(cameraPitch-lookInput.y,-90f,90f);
        //rotation of the cameraPitch degrees around the x axis
        cameraTransform.localRotation=Quaternion.Euler(cameraPitch,0,0);
        //we rotate on the Y axis relativ to the lookInput on the x axis
        transform.Rotate(transform.up,lookInput.x);
    }
    
  }
