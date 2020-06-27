using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus_Two : MonoBehaviour {
    private Animator freeze_anim;
    private GameObject my_player;
    public float speed = 3.0f;
    public float bound_detect = 8.0f;//侦察范围
    float boundX, boundY;//巡逻范围
    float dir;//方向
    float beforeX;
    bool isFrozen;
    int ice_counter = 2;//冰冻子弹可受击数
    int health = 3;//普通子弹可受击数

    void Start() {
        dir = -1.0f;
        boundX = 4.0f;
        boundY = 0.5f;
        isFrozen = false;
        beforeX = transform.position.x;//记录初始x值
        my_player = GameObject.FindWithTag("Player");//TODO: 改为玩家Tag
        freeze_anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (isFrozen) {
            freeze_anim.SetBool("isFrozen", true);
            return;
        }
        float nowX = transform.position.x;
        if (Detect()) {
            //追赶
            Chase();
        }
        else if (System.Math.Abs(beforeX - nowX) > 0.1f) {
            //归位
            findAndFlip(beforeX);
            GoBack();
        }
        else {
            //巡逻或停滞
            ;
        }
    }

    void findAndFlip(float destination) {
        float nowX = transform.position.x;
        if (dir > 0 && destination < nowX
                || dir < 0 && destination > nowX) {
            TurnAround();
        }
    }

    void TurnAround() {
        dir = -dir;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

    bool Detect() {
        Vector3 p = my_player.GetComponent<Transform>().position;
        float nowX = transform.position.x;
        float nowY = transform.position.y;
        if (nowX - bound_detect < p.x && p.x < nowX + bound_detect
            && nowY - boundY < p.y && p.y < nowY + boundY) {
            findAndFlip(p.x);
            return true;
        }
        else
            return false;
    }

    void Chase() {
        speed = 5.0f;//加速
        //向dir方向移动
        transform.Translate(new Vector3(dir, 0, 0) * Time.deltaTime * speed);
    }

    void GoBack() {
        speed = 3.0f;//回归原本速度
        float nowX = transform.position.x;
        dir = (beforeX - nowX) / System.Math.Abs(beforeX - nowX);
        transform.Translate(new Vector3(dir, 0, 0) * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "IceBullet")//TODO: 改为冰冻子弹Tag
        {
            if (--ice_counter == 0)
                isFrozen = true;
        }
        if (coll.collider.tag == "NormalBullet")//TODO: 改为普通子弹Tag
        {
            if (--health == 0)
                Destroy(gameObject);
        }
        if (coll.collider.tag == "Player")//TODO: 改为玩家Tag
        {
            if (isFrozen == false) {
                my_player.GetComponent<Health>().BeDamaged(3);
                Destroy(gameObject);
            }
        }
    }
}
