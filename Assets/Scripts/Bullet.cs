using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_liveTime = 1;
    public int bullet_damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.SendMessage("BeShot", bullet_damage, SendMessageOptions.DontRequireReceiver);
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
