using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasure : MonoBehaviour {
    public string playerName;

    Animator mAnim;
    bool isClose;

    void Start() {
        mAnim = GetComponent<Animator>();
        isClose = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && isClose) {
            mAnim.SetBool("isOn", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == playerName) {
            isClose = true;
        }
    }
}
