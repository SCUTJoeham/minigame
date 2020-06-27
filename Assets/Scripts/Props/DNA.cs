using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public string playerName;

    void Start(){
       
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == playerName) {
            GameObject.Find(playerName).SendMessage("GetDNA", 1);
            Destroy(gameObject, 0.0f);
        }
    }
}
