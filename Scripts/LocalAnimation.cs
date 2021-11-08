using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAnimation : MonoBehaviour
{
    Vector3 localPos;
    bool wasPlaying;
 
    void Awake()
    {
        localPos = transform.position;
        wasPlaying = false;
    }
    void LateUpdate(){
        if (!GetComponent<Animation>().isPlaying&&!wasPlaying)
            return;
    }
}
