using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBox : MonoBehaviour
{
    public float moveTime;//移动时间
    public float distance;//移动距离
    public string boxwaterTag;

    float vel;
    Vector3 stopPos;
    Vector3 startPos;
    bool toStop;
    bool water;//水池是否有水
    bool up;//是否在向上运动
    bool down;//是否在向下运动
    int state;//0代表下沉状态，1代表上浮状态
    void Start(){
        vel = distance/moveTime;
        startPos = transform.position;
        stopPos = startPos;
        stopPos.y = startPos.y + distance;
        toStop = false;
        water = false;
        up = false;
        down = false;
        state = 0;
    }

    // Update is called once per frame
    void Update(){
        GameObject obj = GameObject.FindGameObjectWithTag(boxwaterTag);
        //Debug.Log(water);
        if (isClose(obj)) {
            getWater(true);
        }
        else {
            getWater(false);
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

    bool isClose(GameObject obj) {
        Bounds objBound = obj.GetComponent<Collider2D>().bounds;
        Bounds mBound = GetComponent<Collider2D>().bounds;

        Vector2 newSize = mBound.size;
        newSize.x = newSize.x * 1.5f;
        newSize.y = newSize.y * 1.5f;

        Bounds testBounds = new Bounds(mBound.center, newSize);

        return testBounds.Intersects(objBound);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        if (other.tag == boxwaterTag) {
            getWater(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == boxwaterTag) {
            getWater(false);
        }
    }
}
