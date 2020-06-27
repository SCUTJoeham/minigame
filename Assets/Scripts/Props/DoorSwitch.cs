using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public string monsterTag;
    public string doorName;
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == monsterTag) {
            GameObject.Find(doorName).SendMessage("reciveSw");
        }
    }
}
