using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxRayControl : MonoBehaviour {
    public GameObject ray;
    public float deltaT;//间隔时间
    public float detX;//X方向的需要调整的偏移量
    public float detY;//Y方向的需要调整的偏移量
    
    float t2;
    GameObject mRay;
    Vector2 newPos;
    public string boxTag = "Box";
    public string targetName = "Player";
    int onBoxNum;//正在上面的box数量

    void Start() {
        t2 = 0;
        newPos = new Vector2(transform.position.x + detX, transform.position.y + detY);
        mRay = Instantiate(ray, newPos, transform.rotation);
        onBoxNum = 0;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
        Debug.Log(other.tag);

        if (other.tag == boxTag) {
            onBoxNum += 1;
            Destroy(mRay, 0.0f);
        }
        if(other.name == targetName) {
            //
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == boxTag) {
            onBoxNum -= 1;
            if(onBoxNum <= 0) {
                mRay = Instantiate(ray, newPos, transform.rotation);
            }
        }
    }
}
