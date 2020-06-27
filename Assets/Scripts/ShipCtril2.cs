using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCtril2 : MonoBehaviour
{
    public bool isIn = false;
    public float moveTime = 0;
    //public Vector3 speed = new Vector3(5, 0, 0);
    public float speed;

    bool isGo;
    GameObject m_player;
    Transform trans;
    Vector3 stopPosiiton;

    public AudioSource shipAudio;

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "Player") {
            //Vector2 vel2D = new Vector2(speed.x, speed.y);
            //coll.gameObject.GetComponent<Rigidbody2D>().velocity = coll.gameObject.GetComponent<Rigidbody2D>().velocity + vel2D;
            coll.transform.SetParent(transform);
            isIn = true;
        }
        stopPosiiton = new Vector3(transform.position.x + 300.0f, transform.position.y);
    }

    void OnCollisionExit2D(Collision2D coll) {
        //coll.transform.SetParent(null);
        if (coll.collider.tag == "Player") {
            //Vector2 vel2D = new Vector2(speed.x, speed.y);
            //coll.gameObject.GetComponent<Rigidbody2D>().velocity = coll.gameObject.GetComponent<Rigidbody2D>().velocity - vel2D;
            coll.transform.SetParent(null);
            isIn = false;
        }
    }


    // Start is called before the first frame update
    void Start() {

        m_player = GameObject.FindWithTag("Player");
        trans = this.GetComponent<Transform>();
        isIn = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && m_player.GetComponent<PlayerController>().fuel > 0 && isIn) {
            m_player.SendMessage("UseFuel", null, SendMessageOptions.DontRequireReceiver);
            moveTime += 10.0f;
            isGo = true;
            shipAudio.Play();
        }

        Move();
    }

    void Move() {
        if (isIn && moveTime > 0) {
            Vector3 tempPosition = transform.position;
            tempPosition = Vector3.MoveTowards(tempPosition, stopPosiiton, speed * Time.deltaTime);
            transform.position = tempPosition;
            moveTime -= Time.deltaTime;
            
            //rbody.MovePosition(trans.position + speed * Time.deltaTime);
        }
        if (isGo && moveTime > 0) {
            Vector3 tempPosition = transform.position;
            tempPosition = Vector3.MoveTowards(tempPosition, stopPosiiton, speed * Time.deltaTime);
            transform.position = tempPosition;
            moveTime -= Time.deltaTime;
            
            //rbody.MovePosition(trans.position + speed * Time.deltaTime);
        }
    }
}
