using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalWater : MonoBehaviour
{
    public string targetName;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //if (collider(target)) {
        //}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == targetName) {
            other.gameObject.SendMessage("BeDie");
            //send message to character
        }

    }
}
