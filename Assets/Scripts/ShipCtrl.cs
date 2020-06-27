using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCtrl : MonoBehaviour
{
    public bool isIn = false;
    public float moveTime = 0;
    public Vector3 speed = new Vector3(5, 0, 0);
    GameObject m_player;
    Rigidbody2D rbody;
    Transform trans;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Player")
        {
            //Vector2 vel2D = new Vector2(speed.x, speed.y);
            //coll.gameObject.GetComponent<Rigidbody2D>().velocity = coll.gameObject.GetComponent<Rigidbody2D>().velocity + vel2D;
            Debug.Log("in");
            coll.transform.SetParent(transform);
            isIn = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        //coll.transform.SetParent(null);
        if (coll.collider.tag == "Player")
        {
            //Vector2 vel2D = new Vector2(speed.x, speed.y);
            //coll.gameObject.GetComponent<Rigidbody2D>().velocity = coll.gameObject.GetComponent<Rigidbody2D>().velocity - vel2D;
            coll.transform.SetParent(null);
            isIn = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
        m_player = GameObject.FindWithTag("Player");
        rbody = this.GetComponent<Rigidbody2D>();
        trans = this.GetComponent<Transform>();
        isIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && m_player.GetComponent<PlayerController>().fuel > 0 &&isIn)
        {
            m_player.SendMessage("UseFuel", null, SendMessageOptions.DontRequireReceiver);
            moveTime += 10.0f;
        }

        Move();
    }

    void Move()
    {
        if (isIn && moveTime > 0)
        {
            moveTime -= Time.deltaTime;
            rbody.MovePosition(trans.position + speed * Time.deltaTime);
        }
    }
}
