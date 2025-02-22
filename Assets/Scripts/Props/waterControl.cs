﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterControl : MonoBehaviour
{
    public Vector2 waterPos0;
    public Vector2 waterPos1;
    public Vector2 waterPos2;

    public GameObject water;

    GameObject objWater;
    bool sw0;//开关0的状态
    bool sw1;
    int state;//0代表水在左边，1代表水在中间，2代表水在右边

    public AudioSource waterAudio;

    void Start(){
        sw0 = false;
        sw1 = false;
        state = 0;
        objWater = Instantiate(water, waterPos0, transform.rotation);
    }

    void Update(){
        //Debug.Log(state);
        int prevState = state;


        if (sw0) {//sw0打开时，水从0流向1
            switch (state) {
                case 0:
                    state = 1;
                    break;
                case 1:
                    //state = 0;
                    break;
                case 2:
                    break;
            }
        }
        else if (!sw0) {//sw0关闭时，水从0流向1
            switch (state) {
                case 0:
                    //state = 1;
                    break;
                case 1:
                    state = 0;
                    break;
                case 2:
                    break;
            }
        }

        if (sw1) {//sw1打开时，水从1流向2
            switch (state) {
                case 0:
                    break;
                case 1:
                    state = 2;
                    break;
                case 2:
                    break;
            }
        }
        else if (!sw1) {//sw2关闭时，水从2流向1
            switch (state) {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    state = 1;
                    break;
            }
        }

        if (sw0) {//sw0打开时，水从0流向1
            switch (state) {
                case 0:
                    state = 1;
                    break;
                case 1:
                    //state = 0;
                    break;
                case 2:
                    break;
            }
        }
        else if (!sw0) {//sw0关闭时，水从0流向1
            switch (state) {
                case 0:
                    //state = 1;
                    break;
                case 1:
                    state = 0;
                    break;
                case 2:
                    break;
            }
        }
        //Debug.Log(state);

        if (state != prevState) {
            Debug.Log(state);
            Destroy(objWater, 0.0f);
            switch (prevState) {
                case 0:
                    //box0.SendMessage("getWater", false);
                    break;
                case 1:
                    break;
                case 2:
                    //box2.SendMessage("getWater", false);
                    break;
            }
            switch (state) {
                case 0:
                    objWater = Instantiate(water, waterPos0, transform.rotation);
                    waterAudio.Play();
                    //box0.SendMessage("getWater", true);
                    break;
                case 1:
                    objWater = Instantiate(water, waterPos1, transform.rotation);
                    waterAudio.Play();
                    break;
                case 2:
                    objWater = Instantiate(water, waterPos2, transform.rotation);
                    waterAudio.Play();
                    //box2.SendMessage("getWater", true);
                    break;
            }
        }
    }

    void getMessageFromSw0(bool message) {
        //Debug.Log(state);
        sw0 = message;
    }

    void getMessageFromSw1(bool message) {
        Debug.Log(state);
        sw1 = message;
    }
}
