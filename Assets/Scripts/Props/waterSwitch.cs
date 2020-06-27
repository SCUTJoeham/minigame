using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterSwitch : MonoBehaviour {
    public GameObject player;
    public GameObject switchControl;
    public int switchNum;
    public float openT;//开关动的时间
    public float dist;

    bool isOn;
    Animator mAnim;
    float t1;

    void Start() {
        isOn = false;
        mAnim = this.GetComponent<Animator>();
        t1 = openT;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && isClose(player)) {
            isOn = !isOn;
            if (switchNum == 0) {
                //Debug.Log("0");
                //Debug.Log(isOn);
                switchControl.SendMessage("getMessageFromSw0", isOn);
            }
            else if(switchNum == 1) {
                //Debug.Log("1");
                //Debug.Log(isOn);
                switchControl.SendMessage("getMessageFromSw1", isOn);
            }
            mAnim.SetBool("on", true);
        }
        if (isOn) {
            t1 = t1 - Time.deltaTime;
            if (t1 <= 0) {
                mAnim.SetBool("on", false);
                t1 = openT;
            }
        }
    }

    bool isClose(GameObject obj) {
        //Bounds objBound = obj.GetComponent<Collider2D>().bounds;
        //Bounds gasBound = GetComponent<Collider2D>().bounds;

        //Debug.Log(objBound);
        //Debug.Log(gasBound);

        //Vector2 newSize = gasBound.size;
        //newSize.x = newSize.x * 1.5f;
        //newSize.y = newSize.y * 1.5f;

        //Bounds testBounds = new Bounds(gasBound.center, newSize);

        //return testBounds.Intersects(objBound);

        Bounds objBound = obj.GetComponent<Collider2D>().bounds;
        Bounds gasBound = GetComponent<Collider2D>().bounds;

        float dis = (gasBound.center - objBound.center).magnitude;
        //Debug.Log(dis);
        return (dis < dist);

    }
}
