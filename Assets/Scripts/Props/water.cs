using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public string targetName;

    void Start(){

    }

    // Update is called once per frame
    void Update(){
        //if (collider(target)) {
        //}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == targetName) {
            other.gameObject.SendMessage("BeDie");
            //send message to character
        }
        
    }

    //bool collider(GameObject obj) { //检测碰撞
    //    Bounds objBound = obj.GetComponent<Collider2D>().bounds;
    //    Bounds bullBound = GetComponent<Collider2D>().bounds;

    //    Vector2 newSize = bullBound.size;
    //    newSize.x = newSize.x * 1.2f;
    //    newSize.y = newSize.y * 1.2f;

    //    Bounds testBounds = new Bounds(bullBound.center, newSize);

    //    return testBounds.Intersects(objBound);
    //}
}
