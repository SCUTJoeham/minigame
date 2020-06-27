using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBox : MonoBehaviour
{
    public float moveTime;//移动时间
    public float distance;//移动距离
    public string boxwaterTag;
    public float detT;

    float vel;
    Vector3 stopPos;
    Vector3 startPos;
    bool toStop;
    bool water;//水池是否有水
    bool up;//是否在向上运动
    bool down;//是否在向下运动
    int state;//0代表下沉状态，1代表上浮状态
    float t1;
    float oriM;
    bool confStop;

    void Start(){
        confStop = false;
        vel = distance/moveTime;
        oriM = GetComponent<Rigidbody2D>().mass;
        //startPos = transform.position;
        //stopPos = startPos;
        //stopPos.y = startPos.y + distance;
        toStop = false;
        water = false;
        up = false;
        down = false;
        state = 0;
        t1 = detT;
    }

    // Update is called once per frame
    void Update(){
        GameObject obj = GameObject.FindGameObjectWithTag(boxwaterTag);
        //Debug.Log(water);
        if (closeTo(obj)) {
            if(t1 > 0) {
                t1 -= Time.deltaTime;
            }
            if (t1 < 0) {
                if (!confStop) {
                    confStop = true;
                    startPos = transform.position;
                    stopPos = startPos;
                    stopPos.y = startPos.y + distance;
                }
                getWater(true);
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().mass = 1000;

                //Vector2 f = new Vector2(0, GetComponent<Rigidbody2D>().mass * GetComponent<Rigidbody2D>().gravityScale * 9.81f);
                //GetComponent<Rigidbody2D>().AddForce(f);
            }
            //getWater(true);
            //GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else {
            getWater(false);
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Rigidbody2D>().mass = oriM;

        }

        

        if (water && state == 0) {
            up = true;
        }

        if (!water && state == 1) {
            down = true;
        }

        if (up) {
            Vector3 tempPos = transform.position;
            tempPos = Vector3.MoveTowards(tempPos, stopPos, vel * Time.deltaTime);
            transform.position = tempPos;
            if (transform.position == stopPos) {
                state = 1;
                up = false;
            }
        }
        if (down) {
            Vector3 tempPos = transform.position;
            tempPos = Vector3.MoveTowards(tempPos, startPos, vel * Time.deltaTime);
            transform.position = tempPos;
            if (transform.position == startPos) {
                state = 0;
                down = false;
            }
        }
        //if (water && state == 0) {
        //    Vector3 tempPos = transform.position;
        //    tempPos = Vector3.MoveTowards(tempPos, stopPos, vel * Time.deltaTime);
        //    transform.position = tempPos;
        //    if (transform.position == stopPos) {
        //        state = 1;
        //    }
        //}

        //else if(!water && state == 1) {
        //    Vector3 tempPos = transform.position;
        //    tempPos = Vector3.MoveTowards(tempPos, startPos, vel * Time.deltaTime);
        //    transform.position = tempPos;
        //    if (transform.position == startPos) {
        //        state = 0;
        //        toStop = true;
        //    }
        //}
    }

    void getWater(bool w) {
        water = w;
    }

    //void readyUp() {
    //    getWater(true);
    //    GetComponent<Rigidbody2D>().gravityScale = 0;
    //}

    bool closeTo(GameObject obj) {
        Bounds objBound = obj.GetComponent<Collider2D>().bounds;
        Bounds mBound = GetComponent<Collider2D>().bounds;

        Vector2 newSize = mBound.size;
        newSize.x = newSize.x * 1.1f;
        newSize.y = newSize.y * 1.1f;

        Bounds testBounds = new Bounds(mBound.center, newSize);

        return testBounds.Intersects(objBound);
    }


    //private void OnTriggerEnter2D(Collider2D other) {
    //    if (other.tag == boxwaterTag) {
    //        getWater(true);
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D other) {
    //    if (other.tag == boxwaterTag) {
    //        getWater(true);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other) {
    //    if (other.tag == boxwaterTag) {
    //        getWater(false);
    //    }
    //}
}
