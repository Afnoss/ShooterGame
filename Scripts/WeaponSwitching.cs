using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    // Start is called before the first frame update
    public int selectedWeapon=0;
    void Start()
    {
        //function for selecting the weapon
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //we initialize the previousSelectedWeapon with the selected weapon
        int previousSelectedWeapon=selectedWeapon;
        //if the cound iss less or equal to 0 we terminate execution
        if (Input.touchCount <= 0)
     {
         return;
     }
     //if we double tap we change the weapon
     foreach(Touch touch in Input.touches) {
         if (touch.tapCount == 2) {
            Debug.Log("Double tap");
            if(selectedWeapon>=transform.childCount-1){
                selectedWeapon=0;
            }
            else{
                selectedWeapon++;
            }
            }
         } 
         if(previousSelectedWeapon!= selectedWeapon){
            SelectWeapon();
         }   
     }     
    
    void SelectWeapon(){
        int i=0;
        //we go through every weapon if the id of the selected weapon equals i we make that object active and the other we deactivate
        foreach(Transform weapon in transform){
            if(i==selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            ++i;
        }
    }
}
