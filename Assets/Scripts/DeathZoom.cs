using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoom : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.SendMessage("BeDie", null, SendMessageOptions.DontRequireReceiver);
        }
    }
}
