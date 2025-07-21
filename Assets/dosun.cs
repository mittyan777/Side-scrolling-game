using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dosun : MonoBehaviour
{
   // public Transform objectA; // 距離を測りたいオブジェクトA
    public Transform objectB; // 距離を測りたいオブジェクトB
    [SerializeField]float distance;
    [SerializeField]bool down;
    [SerializeField]bool trigger;
    [SerializeField]float cooltime = 10;
    // Start is called before the first frame update
    void Start()
    {
        down = false;
    }

    // Update is called once per frame
    void Update()
    {
         distance = Vector3.Distance(this.transform.position, objectB.position);
        if(distance < 4.7 && down == false && trigger == false)
        {
            down = true;
            trigger = true;
        }
        if(down == true && transform.position.y > 6.8)
        {
            transform.position -= transform.up * 15 * Time.deltaTime;
        }
        else if(down == false && transform.position.y < 10.9) 
        {
            Invoke("up", 2);

        }
        if(transform.position.y < 6.8)
        {
            down = false;
            Invoke("reset", 5);
        }
      
       
      
       
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
      
    }
    void reset()
    {
        trigger = false;
    }
    void up()
    {
        if (down == false && transform.position.y < 10.9)
        {
            transform.position += transform.up * 15 * Time.deltaTime;
        }
    }
}
