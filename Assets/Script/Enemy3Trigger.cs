using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Trigger : MonoBehaviour
{
    bool Trigger_Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Get_IsNearPlayer()
    {
        return Trigger_Player;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Trigger_Player = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Trigger_Player = false;
        }
    }

}
