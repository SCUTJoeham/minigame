using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlDoor : MonoBehaviour
{
    Animator mAnim;
    bool isOpen;


    void Start(){
        mAnim = GetComponent<Animator>();
        isOpen = false;
    }

    void Update(){
        
    }

    void reciveSw() {
        isOpen = true;
        mAnim.SetBool("open", true);
        GetComponent<Collider2D>().enabled = false;
    }
}
