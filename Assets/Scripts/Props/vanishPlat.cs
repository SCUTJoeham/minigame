using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vanishPlat : MonoBehaviour
{
    public string playerName;
    public float desT;//销毁时间

    void Start(){
        
    }

    void Update(){
        
    }

    //private void OnTriggerEnter2D(Collider2D other) {
    //    if(other.name == playerName) {
    //        Destroy(gameObject, desT);
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D other) {

        //Debug.Log(other.gameObject.name);

        if (other.gameObject.name == playerName) {
            Destroy(gameObject, desT);

        }
        //Destroy(gameObject, desT);
        ////if (other.name == playerName) {
        ////    Destroy(this, desT);
        ////}
    }

}
