using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    Vector2 stv=new Vector2(100,100);
    
    private Vector3 touchposition;
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchposition = Camera.main.ScreenToWorldPoint(touch.position);
            touchposition.z = 0;
            gameObject.transform.position = touchposition;
            if (touch.phase==TouchPhase.Ended)
            {
                gameObject.transform.position = stv;
            }
        }
    }
}
