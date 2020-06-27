using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virus_bullet_controller : MonoBehaviour
{
    //private GameObject my_player;
    int counter = 1000;//子弹生命周期

    void Start()
    {
        //my_player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        if (--counter <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "IceBullet")
            Destroy(gameObject);
        if (coll.collider.tag == "NormalBullet")
            Destroy(gameObject);
        if (coll.collider.tag == "Player")
        {
            coll.collider.SendMessage("BeDamaged", 1, SendMessageOptions.DontRequireReceiver);
            //my_player.GetComponent<Health>().BeDamaged(1);
            Destroy(gameObject);
        }
    }
}
