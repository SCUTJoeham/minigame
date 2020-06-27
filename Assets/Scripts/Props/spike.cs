using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    public string targetName = "Player";
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.name);
        if (other.name == targetName) {
            other.gameObject.SendMessage("BeDamaged", 1, SendMessageOptions.DontRequireReceiver);
            Debug.Log(1);
        }
    }
}
