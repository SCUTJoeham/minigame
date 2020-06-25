using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fortBullet : MonoBehaviour {
    public float vel;
    public string targetName = "Player";
    public string filmName = "Film";

    GameObject target;
    GameObject film;
    Animator mAnim;

    //public float destroyTime;
    void Start() { //
        mAnim = this.GetComponent<Animator>();
        target = GameObject.Find(targetName);
        film = GameObject.Find(filmName);

        Vector3 targetPos = target.transform.position;

        Vector2 dir = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);
        dir.Normalize();

        Vector2 bullVel = new Vector2(dir.x * vel, dir.y * vel);
        GetComponent<Rigidbody2D>().velocity = bullVel;
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    void Update() {
        //transform.Translate(target.transform.position * Time.deltaTime);    //移动  
        //if (collider(target)) {//如果碰到了主角，子弹消失并且sendmessage
        //    //target.SendMessage();
        //    mAnim.SetBool("explosion", true);
        //    Vector2 zero = new Vector2(0, 0);
        //    GetComponent<Rigidbody2D>().velocity = zero;
        //    //Destroy(gameObject, 0f);
        //}
        //if (collider(film)) {//如果碰到了薄膜，子弹消失并且sendmessage
        //    mAnim.SetBool("explosion", true);
        //    Vector2 zero = new Vector2(0, 0);
        //    GetComponent<Rigidbody2D>().velocity = zero;
        //    film.SendMessage("receviceMessage", true);
        //    //Destroy(gameObject, 0f);

        //}
    }

    bool collider(GameObject obj) { //检测碰撞
        Bounds objBound = obj.GetComponent<Collider2D>().bounds;
        Bounds bullBound = GetComponent<Collider2D>().bounds;

        Vector2 newSize = bullBound.size;
        newSize.x = newSize.x * 1.2f;
        newSize.y = newSize.y * 1.2f;

        Bounds testBounds = new Bounds(bullBound.center, newSize);

        return testBounds.Intersects(objBound);
    }

    private void OnDestroy() {
        Destroy(gameObject, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == targetName) {
            //Debug.Log("get player");
            mAnim.SetBool("explosion", true);
            Vector2 zero = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().velocity = zero;

            other.gameObject.SendMessage("BeDamaged",1);

            //Destroy(gameObject, 0f);

        }
        if (other.gameObject.name == filmName) {
            //Debug.Log("get film");

            //如果碰到了薄膜，子弹消失并且sendmessage
            mAnim.SetBool("explosion", true);
            Vector2 zero = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().velocity = zero;
            film.SendMessage("receviceMessage", true);

        }
    }
}

