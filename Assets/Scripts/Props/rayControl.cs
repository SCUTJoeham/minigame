using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayControl : MonoBehaviour {

    public GameObject ray;
    public float deltaT;//间隔时间
    public float detX;//X方向的需要调整的偏移量
    public float detY;//Y方向的需要调整的偏移量

    float t2;

    Vector2 newPos;

    public AudioSource rayAudio;

    void Start() {
        t2 = 0;
        newPos = new Vector2(transform.position.x + detX, transform.position.y + detY);
    }

    // Update is called once per frame
    void Update() {
        t2 = t2 - Time.deltaTime;

        if (t2 <= 0) {
            GameObject ray0 = Instantiate(ray, newPos, transform.rotation);
            rayAudio.Play();
            t2 = deltaT;
        }
    }
}
