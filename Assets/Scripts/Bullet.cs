using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_liveTime = 4;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //collision.SendMessage("BeShot", bullet_damage, SendMessageOptions.DontRequireReceiver);
        if (coll.collider.tag == "Virus")
            Destroy(gameObject);          
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_liveTime -= Time.deltaTime;
        if(m_liveTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
