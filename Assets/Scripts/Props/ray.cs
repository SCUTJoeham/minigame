using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    public string targetName = "Player";
    void Start() {
        //Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == targetName) {
            other.gameObject.SendMessage("BeDie");
        }
    }

    private void OnDestroy() {
        Destroy(gameObject, 0f);
    }

}
