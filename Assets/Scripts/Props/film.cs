using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class film : MonoBehaviour {
    // Start is called before the first frame update
    bool isColide;
    Animator mAnim;

    void Start() {
        mAnim = this.GetComponent<Animator>();
        isColide = false;
    }

    // Update is called once per frame
    void Update() {
        if (isColide) {
            //Destroy(gameObject, 0.0f);
            mAnim.SetBool("explosion", true);
            Destroy(gameObject, 0.5f);

        }
    }
    void receviceMessage(bool c) {
        isColide = c;
    }

    private void OnDestroy() {
        Destroy(gameObject, 0f);
    }
}

