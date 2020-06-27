using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus_One : MonoBehaviour {
    private Animator freeze_anim;
    public Rigidbody2D virus_bullet;
    private GameObject my_player;//玩家
    public float speed = 3.0f;//怪物速度
    public float power = 5.0f;//子弹速度
    public float bound_detect = 8.0f;//侦察范围
    public float boundX = 4.0f;
    float boundY;//巡逻范围
    float dir;//方向
    float beforeX;//初始位置
    bool isFrozen;//冰冻状态
    int ice_counter = 3;//冰冻子弹可受击数
    int health = 5;//普通子弹可受击数
    int shoot_counter;//射速

    void Start() {
        dir = -1.0f;
        boundX = 4.0f;
        boundY = 0.5f;
        shoot_counter = 10;
        isFrozen = false;
        beforeX = transform.position.x;//记录初始x值
        my_player = GameObject.FindWithTag("Player");
        freeze_anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (isFrozen) {
            freeze_anim.SetBool("isFrozen", true);
            return;
        }
        if (Detect()) {
            if (canShoot())
                Shoot();
        }
        else {
            shoot_counter = 10;//重置计数器
            Patrol();
        }
    }

    bool canShoot() {
        if (--shoot_counter <= 0) {
            shoot_counter = 100;
            return true;
        }
        else {
            return false;
        }
    }

    void Shoot() {
        Rigidbody2D clone;
        clone = (Rigidbody2D)Instantiate(virus_bullet, transform.position + new Vector3(dir, 0, 0), transform.rotation);
        clone.velocity = transform.TransformDirection(new Vector2(dir, 0) * power);
    }

    void Patrol() {
        float nowX = transform.position.x;
        float nowY = transform.position.y;
        if (transform.position.x <= beforeX - boundX) {
            nowX = beforeX - boundX;
            TurnAround();
        }
        else if (transform.position.x >= beforeX + boundX) {
            nowX = beforeX + boundX;
            TurnAround();
        }
        transform.position = new Vector2(nowX, nowY);//越界归位
        //向dir方向移动
        transform.Translate(new Vector3(dir, 0, 0) * Time.deltaTime * speed);
    }

    bool Detect() {
        Vector3 p = my_player.GetComponent<Transform>().position;
        float nowX = transform.position.x;
        float nowY = transform.position.y;
        if (nowX - bound_detect < p.x && p.x < nowX + bound_detect
            && nowY - boundY < p.y && p.y < nowY + boundY) {
            findAndFlip();
            return true;
        }
        else
            return false;
    }

    void findAndFlip() {
        Vector3 p = my_player.GetComponent<Transform>().position;
        float nowX = transform.position.x;
        if (dir > 0 && p.x < nowX
                || dir < 0 && p.x > nowX) {
            TurnAround();
        }
    }

    void TurnAround() {
        dir = -dir;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "IceBullet")//TODO: 改为冰冻子弹Tag
        {
            if (--ice_counter <= 0)
                isFrozen = true;
        }
        if (coll.collider.tag == "NormalBullet")//TODO: 改为普通子弹Tag
        {
            if (--health <= 0)
                Destroy(gameObject);
        }
        if (coll.collider.tag == "Player")//TODO: 改为玩家Tag
        {
            if (isFrozen == false) {
                my_player.GetComponent<Health>().BeDamaged(1);
                Destroy(gameObject);
            }
        }
    }
}
