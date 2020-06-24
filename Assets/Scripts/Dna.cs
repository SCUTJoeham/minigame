using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dna : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.SendMessage("GetDNA", 1, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
