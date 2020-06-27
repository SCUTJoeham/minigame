using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    bool isOpen;
    bool isClose;
    Animator mAnim;
    //BoxCollider2D childrenBound;
    public string childName;

    GameObject child;
    void Start(){
        //childrenBound = GetComponentInChildren<BoxCollider2D>();
        child = transform.Find(childName).gameObject;
        //Debug.Log(child.name);

        //child = GetComponentInChildren<Transform>().gameObject;
        mAnim = GetComponent<Animator>();
        isOpen = false;
        isClose = false;
        child = GameObject.Find(childName);
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.F) && isClose) {
            //Debug.Log("sddd");
            mAnim.SetBool("open", true);
            //child.GetComponent<BoxCollider2D>.
            child.SetActive(false);
            //childrenBound.enabled = false;
        }
   
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("close");
        isClose = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isClose = false;
    }
}
