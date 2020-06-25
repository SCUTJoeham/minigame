using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fort : MonoBehaviour {
    public GameObject fortBullet;
    public GameObject player;
    public float deltaT;//发射炮弹的间隔时间   
    public float fireT;//发射炮弹的开火时间
    public float fireDetT;//发射的延迟时间
    public float leftBound;//最左边的范围
    public float rightBound;//最右边的范围

    float t2;
    float t3;
    float t4;
    bool isFire;
    Animator mAnim;

    void Start() {
        t4 = fireDetT;
        t3 = fireT;
        t2 = deltaT;
        mAnim = GetComponent<Animator>();
        isFire = false;
        InvokeRepeating("upd", deltaT, deltaT);
    }

    private void upd() {
        //Debug.Log(player.transform.position.x);
        if(player.transform.position.x > leftBound && player.transform.position.x < rightBound) {
            mAnim.SetBool("fire", true);
            Invoke("fireOff", fireT);
            isFire = true;
            Vector2 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 120.0f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Invoke("fire", fireDetT);
        }

        //mAnim.SetBool("fire", true);
        //Invoke("fireOff", fireT);
        //isFire = true;
        //Vector2 direction = player.transform.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg  - 120.0f;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //Invoke("fire", fireDetT);
    }

    private void fireOff() {
        mAnim.SetBool("fire", false);
    }

    private void fire() {
        GameObject controlBull = Instantiate(fortBullet, transform.position, transform.rotation);
    }
    // Update is called once per frame
    void Update() {
        //t2 = t2 - Time.deltaTime;
        //t3 = t3 - Time.deltaTime;
        //if (t2 <= 0) {
        //    mAnim.SetBool("fire", true);

        //    isFire = true;
        //    Vector2 direction = player.transform.position - transform.position;
        //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //    GameObject controlBull = Instantiate(fortBullet, transform.position, transform.rotation);

        //    t2 = deltaT;
        //}
        //if (isFire) {
        //    isFire = false;
        //    t3 = fireT;
        //}
        //if (t3 <= 0 && mAnim.GetBool("fire")) {
        //    mAnim.SetBool("fire", false);
        //}

        
       
        
    }

   

}
