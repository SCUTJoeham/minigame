using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update(){
        
    }
}
