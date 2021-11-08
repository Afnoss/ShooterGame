using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class LookAt : MonoBehaviour
 {
     public GameObject target;
     public float turnRate;
 
     void Update()
     {
         Vector3 targetDelta = target.transform.position - transform.position;
         float angleToTarget = Vector3.Angle(transform.forward, targetDelta);
         Vector3 turnAxis = Vector3.Cross(transform.forward, targetDelta);
 
         transform.RotateAround(transform.position, turnAxis, Time.deltaTime * turnRate * angleToTarget);
     }
 }
