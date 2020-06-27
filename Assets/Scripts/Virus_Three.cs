using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus_Three : MonoBehaviour {
    private Animator freeze_anim;
    public GameObject BoomAnim;
    private GameObject clone;
    private GameObject my_player;//玩家
    public float speed = 3.0f;//怪物速度
    public float power = 5.0f;//子弹速度
    public float bound_detect = 64.0f;//侦察范围


    float boundX, boundY;//巡逻范围
    float dir;//方向
    float beforeX;//初始位置
    bool isFrozen;//冰冻状态
    int ice_counter = 1;//冰冻子弹可受击数
    int health = 2;//普通子弹可受击数
    int shoot_counter;//射速
    bool isBoom = false;

    protected AudioSource exploAudio;

    void Start() {
        dir = -1.0f;
        boundX = 4.0f;
        boundY = 0.5f;
        shoot_counter = 10;
        isFrozen = false;
        beforeX = transform.position.x;//记录初始x值
        my_player = GameObject.FindWithTag("Player");//TODO: 改为玩家Tag
        freeze_anim = GetComponent<Animator>();
        exploAudio = GetComponent<AudioSource>();
    }

    void Update() {
        if (isFrozen) {
            freeze_anim.SetBool("isFrozen", true);
            return;
        }
        Detect();
    }

    bool Detect() {
        Vector3 p = my_player.GetComponent<Transform>().position;
        float nowX = transform.position.x;
        float nowY = transform.position.y;
        float dist = (p.x - nowX) * (p.x - nowX) + (p.y - nowY) * (p.y - nowY);
        if (dist <= bound_detect) {
            //transform.Translate((p - transform.position) * Time.deltaTime * 20.0f);
            Boom();
            return true;
        }
        else
            return false;
    }

    void Boom() {
        if (isBoom == true) return;
        isBoom = true;
        clone = Instantiate(BoomAnim, transform.position, transform.rotation);
        GameObject boom_anim = clone;
        foreach (Transform child in clone.GetComponent<Transform>()) {
            boom_anim = child.gameObject;
        }
        AnimatorStateInfo anim_state = boom_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0); ;
        while (anim_state.normalizedTime >= 1.0f) {
            anim_state = boom_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        }
        Destroy(clone, 1.0f);
        my_player.GetComponent<Health>().BeDamaged(5);
        exploAudio.Play();
        Destroy(gameObject, 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "IceBullet")//TODO: 改为冰冻子弹Tag
        {
            if (--ice_counter == 0) {
                isFrozen = true;
                gameObject.layer = 8;
            }
        }
        if (coll.collider.tag == "NormalBullet")//TODO: 改为普通子弹Tag
        {
            if (--health == 0)
                Destroy(gameObject);
        }
        if (coll.collider.tag == "Player")//TODO: 改为玩家Tag
        {
            if (isFrozen == false) {
                my_player.GetComponent<Health>().BeDamaged(5);
                Destroy(gameObject);
            }
        }
    }
}
