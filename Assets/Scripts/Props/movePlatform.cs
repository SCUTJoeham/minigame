using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatform : MonoBehaviour {

    public Vector3 stopPosiiton;
    public float moveTime;
    public float stayTime;
    public string playerName;

    bool toStop = true;         // 是否朝结束位置移动
    float speed;                // 移动的速度
    Vector3 startPostion;

    bool isOn = true;
    void Start() {
        startPostion = transform.position;
        speed = Vector3.Distance(transform.position, stopPosiiton) / moveTime;
    }
    void Update() {
        //if (onPlat(player)) {
        //    OnTriggerEnter(player);
        //    isOn = true;
        //}
        //else if (isOn && !onPlat(player)) {
        //    isOn = false;
        //    OnTriggerExit(player);
        //}
        PlatformMoveOn();
    }

    void PlatformMoveOn() {
        StartCoroutine(PlatformMove(stopPosiiton));
    }

    IEnumerator PlatformMove(Vector3 stopPosiiton) {
        Vector3 tempPosition = transform.position;
        if (toStop) {
            tempPosition = Vector3.MoveTowards(tempPosition, stopPosiiton, speed * Time.deltaTime);
            transform.position = tempPosition;
            if (transform.position == stopPosiiton) {
                yield return new WaitForSeconds(stayTime);
                toStop = false;
            }
        }
        else if (!toStop) {
            tempPosition = Vector3.MoveTowards(tempPosition, startPostion, speed * Time.deltaTime);
            transform.position = tempPosition;
            if (transform.position == startPostion) {
                yield return new WaitForSeconds(stayTime);
                toStop = true;
            }
        }
    }

    // 相对运动



    //private void OnTriggerEnter(Collider other) {
    //    if (other.name == playerName) {
    //        GameObject player = GameObject.Find(playerName);
    //        player.transform.SetParent(transform);
    //    }
    //    //other.transform.SetParent(transform);
    //    Debug.Log(other.name);
    //}

    //private void OnTriggerExit(Collider other) {
    //    if (other.name == playerName) {
    //        GameObject player = GameObject.Find(playerName);
    //        player.transform.SetParent(null);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D other) {

        other.transform.SetParent(transform);
        Debug.Log(other);
    }

    private void OnCollisionExit2D(Collision2D other) {
        other.transform.SetParent(null);

    }

    //private void OnTriggerEnter2D(Collider2D other) {
    //    if (other.name == playerName) {
    //        GameObject player = GameObject.Find(playerName);
    //        player.transform.SetParent(transform);
    //    }
    //    //other.transform.SetParent(transform);
    //    Debug.Log(other.name);
    //}

    //private void OnTriggerExit2D(Collider2D other) {
    //    if (other.name == playerName) {
    //        GameObject player = GameObject.Find(playerName);
    //        player.transform.SetParent(null);
    //    }
    //}

    //bool onPlat(GameObject obj) {
    //    Bounds bound = obj.gameObject.GetComponent<BoxCollider2D>().bounds;
    //    float range = bound.size.y * 0.2f;
    //    Vector2 v = new Vector2(bound.center.x, bound.min.y - range);
    //    RaycastHit2D hit = Physics2D.Linecast(v, bound.center);
    //    return (hit.collider.gameObject == this.gameObject);
    //}

}

